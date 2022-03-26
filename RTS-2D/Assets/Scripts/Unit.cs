using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Unit : MonoBehaviour
{

    private SpriteRenderer sprRenderer;
    Vector2 endPoint;
    private GridController gridController;
    private Rigidbody2D body;
    List<Point> path;

    bool isMoving;
    void Start()
    {


        isMoving =false;
        GameObject it = GameObject.Find("GridController");
        body = this.GetComponent<Rigidbody2D>();
        gridController = it.GetComponent<GridController>();

        Selection.Instance.unitList.Add(this.gameObject);
        endPoint = transform.position;
        sprRenderer = GetComponent<SpriteRenderer>();

        // create source and target points
        //Point _from = new Point(1, 1);
        //Point _to = new Point(10, 10);

        // get path
        // path will either be a list of Points (x, y), or an empty list if no path is found.

    }

    private async void Update()
    {
        if(isMoving){
         //   Debug.Log("____________HEY____________");
        foreach (var it in path)
        {
            UnitMovement(new Vector2((it.x),(it.y)));
        }
        }
    }
    private void OnDestroy()
    {
        Selection.Instance.unitList.Remove(this.gameObject);
    }

    /*
     * Wykonywana gdy jednostka jest zaznaczana przez skrypt Selection
     */
    private void OnSelect()
    {
        sprRenderer.color = new Color(1f, 0f, 0f, 1f);
    }

    /*
     * Wykonywana gdy jednostka jest odznaczana przez skrypt Selection
     */
    private void OnDeselect()
    {
        sprRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void CreatePath(Point _from, Point _to, Vector2 mousePos)
    {
        //Debug.Log("PATH---->");
        endPoint = mousePos;
        path = Pathfinding.FindPath(gridController.grid, _from, _to);
        foreach (var it in path)
        {
            Debug.Log(new Vector2(it.x,it.y));
        }
        //Debug.Log("SUPEEER END PATH---->");
        isMoving =true;

    }

    public void UnitMovement(Vector2 end)
    {
        Debug.Log("TO MOVEING______>");
        end= new Vector2(end.x+0.5f,end.x + 0.5f );
        Debug.Log(end);
        Vector3 pos = Vector3.MoveTowards(transform.position, new Vector3(end.x, end.y, 0), 5 * Time.deltaTime);
        body.MovePosition(pos);
    }

}
