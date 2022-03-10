using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoxesJumpingManager : MonoBehaviour
{
    [SerializeField] private int SpawnRangeXStart, SpawnRangeXEnd;
    [SerializeField] private float DistanceBetweenBoxes;
    [SerializeField] private GameObject CubePrefab;
    [SerializeField] private GameObject CubeContainer;
    [SerializeField] public Color[] colorsToSet;
    [SerializeField] public int cubeDenom;
    [SerializeField] public float jumpAmount;

    private List<CubeWithIDAndColor> cubeList;
    private int currentColorIndex;
    
    private int currentColorCube;
    private int currentJumpCube;
    private bool cubesChangingColors = false;
    private bool cubesJumping = false;
    void Start()
    {
        cubeList = new List<CubeWithIDAndColor>();
        currentColorIndex = 0;
        currentColorCube = 0;
        currentJumpCube = 0;
        int i = 0;
        while ((i * DistanceBetweenBoxes + SpawnRangeXStart) < SpawnRangeXEnd)
        {
            GameObject currentBox = Instantiate(CubePrefab, CubeContainer.transform);
            CubeWithIDAndColor currentCubeWithIDAndColor = currentBox.GetComponent<CubeWithIDAndColor>();
            currentBox.transform.position = new Vector3((i * DistanceBetweenBoxes + SpawnRangeXStart), 1, 0);
            currentCubeWithIDAndColor.id = i;
            cubeList.Add(currentCubeWithIDAndColor);
            i++;
        }
    }

    private void Update()
    {
        if (!cubesChangingColors)
        {
            cubesChangingColors = true;
            var handle = StartCoroutine(ChangeCubeColor());
        }
        if (!cubesJumping)
        {
            cubesJumping = true;
            var handle = StartCoroutine(JumpCubes());
        }
    }
    
    private IEnumerator JumpCubes()
    {
        CubeWithIDAndColor myCube = cubeList.First(cube => currentJumpCube % CubeContainer.transform.childCount == cube.id);
        myCube.makeAJump(jumpAmount);
        currentJumpCube++;
        yield return new WaitForSeconds(0.75f);
        cubesJumping = false;
    }

    private IEnumerator ChangeCubeColor()
    {
        foreach (CubeWithIDAndColor currentCube in cubeList
                     .Where(cube => currentColorCube % cubeDenom == cube.id % cubeDenom))
        {
            currentCube.changeColor(colorsToSet[currentColorIndex]);
        }
        currentColorIndex = (currentColorIndex + 1) % colorsToSet.Length;
        currentColorCube++;
        yield return new WaitForSeconds(1);
        cubesChangingColors = false;
    }
}
