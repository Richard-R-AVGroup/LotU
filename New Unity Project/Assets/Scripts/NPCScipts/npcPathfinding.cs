using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcPathfinding : MonoBehaviour
{

    public Transform goal;
    NavMeshAgent agent;

    public float navWait;
    public bool enterDoor;

    public bool shopkeeper;
    public bool working;
    public bool active;
    public bool talking;

    public GameObject ownedStore;
    public Transform[] owned;
    public List<GameObject> workObjects = new List<GameObject>();

    public float workingTime;
    public int randLocation;
    public GameObject selectedObj;
    public bool timeWait;

    // Start is called before the first frame update
    void Start()
    {
        findBusiness();                                 //function to find a business not owened
        agent = this.GetComponent<NavMeshAgent>();      //find this navmeshagent component for pathfinding
        agent.destination = goal.position;              //setting the object location to the navmesh destination
        navWait = 1;                                    //door waiting time (Seconds)
        enterDoor = false;                              //bool to show if the npc is going through a door
        workingTime = Random.Range(3, 6);               //time int to determine when the npc should change position
    }

    // Update is called once per frame
    void Update()
    {
        if (enterDoor == true)                      //if entering a door
            enteringDoor();                         //start door opening behaviour

        if (talking == true)                        //if NPC is talking to a player
            isTalking();                            //start talking behaviour
        else
        if (Vector3.Distance(this.transform.position, goal.transform.position) < 0.8f)   // if the npc is close enough to the goal and working == false (working might be redundant)
        {
            agent.speed = 0;                            //npc pathfinding speed = 0
            if (workingTime >= 0)                       //if the workingTime timer reaches 0 or less 
                agent.speed = 3.5f;                     //set the pathfinding speed to the normal walking speed;
            if (goal.name == "ShopkeepPosition")        //if the npc gets to the shopkeepers location of the object
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, goal.gameObject.transform.parent.rotation, 2f * Time.deltaTime);
                if (workObjects.Capacity == 0)          //if the store hasnt found the working objects yet
                {
                    working = true;                     //reset the working var
                    findWorkObj();                      //find all the working objects associated with the store and adding them to a list
                }
            }
        }

        if (working == true && active != true)          //if working == true and active !=
        {
            
            workingTime -= Time.deltaTime;              //start the working time timer
            if (workingTime <= 0)                       //if it reaches 0 or less than
            {
                agent.speed = 3.5f;                                                                                 //reset the npc speed
                randLocation = (int)Mathf.Floor(Random.Range(0, workObjects.Capacity-1));                             //set a random int based on the capacity of the objects available
                goal = workObjects[randLocation].GetComponentInChildren<Transform>().Find("ShopkeepPosition");      //set the pathfinding goal by finding the object in the list from the randLocation int and searching for the shopKeepLocation child position within the object
                active = true;                                                                                      //set active true as the npc is actively moving
                working = false;                                                                                    //set working to false as the npc is moving
                workingTime = Random.Range(6,16);                                                                   //reset the workingTime timer 
            }                
        }

        else if (working == false && active == true)
        {
            agent.destination = goal.position;
            active = false;
            working = true;
        }


    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Door")
        {
            enterDoor = true;
            collision.gameObject.GetComponent<doorBehaviour>().open = true;
        }
    }

    void enteringDoor()
    {
        //Debug.Log("hit");
        agent.speed = 0;
        navWait -= Time.deltaTime;
        if(navWait <= 0)
        {
            agent.speed = 3.5f;
            enterDoor = false;
            navWait = 0;
        }
            
    }

    public void findBusiness()
    {
        ownedStore = GameObject.Find("Store");

        for(int i = 0; i <= ownedStore.GetComponentsInChildren<Transform>().Length; i++)
        {
            owned = ownedStore.GetComponentsInChildren<Transform>();
            for (int b = 0; b <= owned.Length; b++)
                if (owned[b].gameObject.name == "ShopkeepPosition")
                {
                    goal = owned[b].transform;
                    return;
                }
        }
            if (ownedStore.GetComponent<storeBehaviour>().owner != null)
                ownedStore = GameObject.Find("Store" + 1);
    }

    public void findWorkObj()
    {
        if (owned != null)
        {
            for (int i = 0; i <= owned.Length - 1; i++)
            {
                switch (owned[i].gameObject.name)
                {
                    case "ShopkeepCounter":
                        workObjects.Add(owned[i].gameObject);
                        break;
                    case "Shelves":
                        workObjects.Add(owned[i].gameObject);
                        break;
                    case "Shelves (1)":
                        workObjects.Add(owned[i].gameObject);
                        break;
                    case "Shelves (2)":
                        workObjects.Add(owned[i].gameObject);
                        break;
                    default:
                        //randLocation = (int)Mathf.Floor(Random.Range(0, workObjects.Capacity));
                        break;
                }
            }
        }
    }
    
    public void isTalking()
    {
        agent.speed = 0;
        working = false;
        
        if (goal.tag == "Player")
        {
            //transform.LookAt(new Vector3(goal.position.x, this.transform.position.y, goal.position.z));
            Quaternion target = Quaternion.LookRotation(goal.position - this.transform.position, Vector3.left);
            target.x = 0;
            target.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 8);
        }
        
        workingTime = 0;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            talking = false;
            working = true;
            workingTime = Random.Range(6, 16);
            agent.speed = 3.5f;
            return;
        }
    }
}
