using System.Collections;
using System.Collections.Generic;
    
    public class Node
    {
        public int x;
        public int y;

        public bool isObstacle =false;
        public bool isVisited =false;
    
        public float globalGoal;
        public float localGoal;
        public List<Node> neighBours;
        public Node parent;

        

        public Node(int iX, int iY)
        {
            neighBours = new List<Node>();
            this.x = iX;
            this.y = iY;
        }

        public Node(Node b)
        {
            x = b.x;
            y = b.y;
        }
    }

