using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    #region FIELD
    public static GameController _instance;
    [SerializeField]
    private List<NodePosition> listNodePosition;
    [SerializeField]
    private Button buttonLeft;
    [SerializeField]
    private Button buttonRight;
    [SerializeField]
    private GameObject playerObject;

    private NodePosition nextLeftNode = new NodePosition();
    private NodePosition nextRightNode = new NodePosition();
    private Vector3 nextRightPos = new Vector3();
    private Vector3 nextLeftPos = new Vector3();
    #endregion
    #region ENUM
    public enum LandType
    {
        LEFT,
        RIGHT
    }
    #endregion
    #region PROPERTY
    public static GameController Instance { get { return _instance; } }
    public List<NodePosition> ListNodePositions { get { return this.listNodePosition; } set { this.listNodePosition = value; } }
    #endregion
    #region MONO
    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        this.buttonLeft.onClick.AddListener(this.onMoveLeft);
        this.buttonRight.onClick.AddListener(this.onMoveRight);
    }

    private void OnDisable()
    {
        this.buttonLeft.onClick.RemoveListener(this.onMoveLeft);
        this.buttonRight.onClick.RemoveListener(this.onMoveRight);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            this.onMoveLeft();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            this.onMoveRight();
    }
    #endregion
    #region METHOD
    private void onMoveRight()
    {
        this.findNextNodePosition();
        if (this.nextRightNode == null) Debug.Break();
        this.playerObject.transform.position = this.nextRightNode.transform.position;
        
        for(int i = 0; i < this.listNodePosition.Count; i++)
        {
            if (this.listNodePosition[i].transform.position.y <= this.nextRightNode.transform.position.y)
                this.listNodePosition.Remove(this.listNodePosition[i]);
        }
    }

    private void onMoveLeft()
    {
        this.findNextNodePosition();
        if (this.nextLeftNode == null) Debug.Break();
        this.playerObject.transform.position = this.nextLeftNode.transform.position;
        
        for (int i = 0; i < this.listNodePosition.Count; i++)
        {
            if (this.listNodePosition[i].transform.position.y <= this.nextLeftNode.transform.position.y)
                this.listNodePosition.Remove(this.listNodePosition[i]);
        }
    }

    private void findNextNodePosition()
    {
        this.nextLeftNode = this.listNodePosition.Find(node => node.LantypeCurrent == LandType.LEFT && node.transform.position.y > this.playerObject.transform.position.y);
        this.nextRightNode = this.listNodePosition.Find(node => node.LantypeCurrent == LandType.RIGHT && node.transform.position.y > this.playerObject.transform.position.y);

        if (this.nextLeftNode == null) return;
        this.nextLeftPos = this.nextLeftNode.transform.position;
        if (this.nextRightNode == null) return;
        this.nextRightPos = this.nextRightNode.transform.position;

        if (this.nextLeftNode.transform.position.y > this.nextRightNode.transform.position.y)
            this.nextLeftNode = null;
        else if (this.nextLeftNode.transform.position.y < this.nextRightNode.transform.position.y)
            this.nextRightNode = null;
    }
    #endregion
}
