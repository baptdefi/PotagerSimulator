using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/**
 * Cette classe permet de créer un rayon partant de la caméra en direction de la position du curseur dans l'environnement 3D.
 * L'objet portant se script peut saisir des objets, les manipuler et les déplacer grace au clique gauche de la souris.
 * Trois curseurs sont implémentés :
 * Un curseur cursorOff lorsqu'aucun objet manipulable (tag "Draggable") n'est détecté par le rayon.
 * Un curseur cursorDraggable lorsqu'un objet manipulable est détécté mais non saisi
 * Un curseur cursorDragged lorsqu'un obhet est saisi
**/
public class RayCasting : MonoBehaviour
{

	private float distanceToObj;	// Distance entre le personnage et l'objet saisi
	private Rigidbody attachedObject;	// Objet saisi, null si aucun objet saisi

	public const int RAYCASTLENGTH = 100;	// Longueur du rayon issu de la caméra

	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotSpot = new Vector2(16, 16);	// Offset du centre du curseur
	public Texture2D cursorOff, cursorDragged, cursorDraggable; // Textures à appliquer aux curseurs
    public Texture2D pastDurableCursor;

    private bool isDraggable = false;
    private bool isControl = false;

    private bool bWateringCanRotated = false;
    private bool bShovellRotated = false;

    void Start ()
	{
		distanceToObj = -1;
		Cursor.SetCursor (cursorOff, hotSpot, cursorMode);
		Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        pastDurableCursor = cursorOff;
    }

	void Update ()
	{
        // Reset the game
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.LoadScene("mainscene");
        }

        // Le raycast attache un objet cliqué
        RaycastHit hitInfo;
		Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay (ray.origin, ray.direction * RAYCASTLENGTH, Color.blue);
		bool rayCasted = Physics.Raycast (ray, out hitInfo, RAYCASTLENGTH);

        Vector3 right = GetComponentInChildren<Camera>().transform.right;

        // Cursor
        if (rayCasted)
        {
            if (hitInfo.transform.CompareTag("Soil") || hitInfo.transform.CompareTag("Mold"))
            {
                if (attachedObject != null && (attachedObject.gameObject.name == "SmallShovel" || attachedObject.gameObject.name == "Shovel"))
                {
                    Cursor.SetCursor(cursorDraggable, hotSpot, cursorMode);
                }
            }
            else if(hitInfo.transform.CompareTag("Draggable") || hitInfo.transform.CompareTag("Controls"))
            {
                if(attachedObject == null)
                {
                    Cursor.SetCursor(cursorDraggable, hotSpot, cursorMode);
                }         
            }
            else
            {
                Cursor.SetCursor(pastDurableCursor, hotSpot, cursorMode);
            }
        }

        // Move the attached draggable object
        if (attachedObject != null && isDraggable)
        {
            attachedObject.transform.position = ray.origin + (ray.direction * distanceToObj) + (right * 0.4f);
        }

        // WateringCan case
        if (attachedObject != null && attachedObject.gameObject.name == "WateringCan")
        {
            // Update rotation according to camera
            if (bWateringCanRotated)
            {
                Quaternion newRotation = Quaternion.LookRotation(ray.direction);
                newRotation *= Quaternion.Euler(0, 0, 60);
                attachedObject.gameObject.transform.rotation = newRotation;
            }
            else
            {
                Quaternion newRotation = Quaternion.LookRotation(ray.direction);
                attachedObject.gameObject.transform.rotation = newRotation;
            }

            // Rotate to use it
            if (Input.GetMouseButtonUp(1))
            {
                if (bWateringCanRotated)
                    attachedObject.gameObject.transform.Rotate(new Vector3(0, 0, -60));
                else
                    attachedObject.gameObject.transform.Rotate(new Vector3(0, 0, 60));
                bWateringCanRotated = !bWateringCanRotated;
            }
        }

        // Shovel case
        if (attachedObject != null && (attachedObject.gameObject.name == "SmallShovel" || attachedObject.gameObject.name == "Shovel"))
        {
            // Update rotation according to camera
            if (bShovellRotated)
            {
                Quaternion newRotation = Quaternion.LookRotation(ray.direction);
                newRotation *= Quaternion.Euler(0, 180, 0);
                newRotation *= Quaternion.Euler(20, 0, 0);
                newRotation *= Quaternion.Euler(0, 0, -60);
                attachedObject.gameObject.transform.rotation = newRotation;
            }
            else
            {
                Quaternion newRotation = Quaternion.LookRotation(ray.direction);
                newRotation *= Quaternion.Euler(0, 180, 0);
                newRotation *= Quaternion.Euler(20, 0, 0);
                attachedObject.gameObject.transform.rotation = newRotation;
            }

            // Try to dig
            if (Input.GetMouseButtonUp(1))
            {
                if (!attachedObject.gameObject.GetComponent<ShovelScript>().IsCubeAlready() && !bShovellRotated)
                {
                    if (hitInfo.transform.CompareTag("Soil"))
                    {
                        hitInfo.collider.gameObject.GetComponent<EarthSoilScript>().ExtractDirt(attachedObject.transform);
                    }

                    if (hitInfo.transform.CompareTag("Mold"))
                    {
                        hitInfo.collider.gameObject.GetComponent<MoldScript>().ExtractDirt(attachedObject.transform);
                    }
                }
            }

            // Rotate to drop dirt cube
            if (Input.GetButtonDown("Action"))
            {
                if (bShovellRotated)
                    attachedObject.gameObject.transform.Rotate(new Vector3(0, 0, 60));
                else
                    attachedObject.gameObject.transform.Rotate(new Vector3(0, 0, -60));
                bShovellRotated = !bShovellRotated;

                attachedObject.gameObject.GetComponent<ShovelScript>().FreeCube();
            }
        }

        if (Input.GetMouseButtonUp(0)) // Click relaché
        {
            if (attachedObject != null)
            {
                // Drop draggable
                if (isDraggable)
                {
                    distanceToObj = 1.0f;
                    attachedObject.transform.position = ray.origin + (ray.direction * distanceToObj);

                    attachedObject.isKinematic = false;
                    attachedObject = null;

                    isDraggable = false;

                    bShovellRotated = false;
                    bWateringCanRotated = false;

                    Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
                    pastDurableCursor = cursorOff;
                }

                // Let control
                if (isControl)
                {
                    attachedObject.isKinematic = false;
                    attachedObject = null;

                    isControl = false;

                    Cursor.SetCursor(cursorOff, hotSpot, cursorMode);
                    pastDurableCursor = cursorOff;
                }
            }
            else
            {
                // If an object is hit.
                if (rayCasted)
                {
                    if (hitInfo.transform.CompareTag("Draggable"))
                    {
                        // Ignore planted seeds and attach draggable
                        if (!hitInfo.transform.gameObject.name.Contains("seed") || !hitInfo.transform.gameObject.GetComponent<PlantScript>().IsPlanted)
                        {
                            isDraggable = true;

                            attachedObject = hitInfo.rigidbody;
                            attachedObject.isKinematic = true;

                            distanceToObj = 0.55f;
                            attachedObject.transform.position = ray.origin + (ray.direction * distanceToObj) + (right * 0.4f);

                            attachedObject.gameObject.transform.rotation = Quaternion.LookRotation(ray.direction);

                            Cursor.SetCursor(cursorDragged, hotSpot, cursorMode);
                            pastDurableCursor = cursorDragged;
                        }
                    }
                }
            }
        }

        else if (Input.GetMouseButtonDown(0))
        {
            //// Simulate the watering.
            //if (hitInfo.transform.CompareTag("Soil"))
            //{
            //    // TODO: Changer ça pour vérifier qu'on tient bien un arrosoir par exemple.
            //    hitInfo.collider.gameObject.GetComponent<SoilScript>().Water(0.1f);
            //}

            if (attachedObject == null)
            {
                // If an object is hit.
                if (rayCasted)
                {
                    // set control object
                    if (hitInfo.transform.CompareTag("Controls"))
                    {
                        isControl = true;

                        attachedObject = hitInfo.rigidbody;
                        attachedObject.isKinematic = true;

                        distanceToObj = hitInfo.distance;

                        Cursor.SetCursor(cursorDragged, hotSpot, cursorMode);
                        pastDurableCursor = cursorDragged;
                    }
                }
            }
        }

        else if (Input.GetMouseButton(0) && attachedObject != null) // L'utilisateur continue la saisie d'un objet control
        {
            // Move control object
            if (isControl)
            {
                attachedObject.MovePosition(ray.origin + (ray.direction * distanceToObj));
            }
        }
	}
}
