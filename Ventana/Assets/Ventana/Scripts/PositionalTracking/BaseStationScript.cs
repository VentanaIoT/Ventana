using System.Text;
using UnityEngine;

public class BaseStationScript : MonoBehaviour {
    [Tooltip("object representing base station")]
    public GameObject baseStation;
    public int BaseStationNumber = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnSliderChangeRequest(KnobHandler.SliderLevels levels) {
        //Debug.Log("X: " + levels.XAxisLevel + " Y: " + levels.YAxisLevel + " Z: "+ levels.ZAxisLevel);
        float divisor = 5;
        Vector3 levelVector = new Vector3(levels.XAxisLevel / divisor,
                                          levels.YAxisLevel / divisor,
                                          levels.ZAxisLevel / divisor);
        //just change the rotation based on the level... lets go slow cause im slow
        baseStation.transform.Rotate(levelVector);
    }

    public void GenerateBaseStationMatrix() {
        StringBuilder configuration = new StringBuilder();
        configuration.Append("b");
        configuration.Append(BaseStationNumber);

        Vector3 coordinates = baseStation.transform.position;
        configuration.Append(" " + coordinates.x.ToString());
        configuration.Append(" " + coordinates.y.ToString());
        configuration.Append(" " + coordinates.z.ToString());
        Vector3 degreesRotation = baseStation.transform.rotation.eulerAngles;

        Vector3 rotation = new Vector3( (degreesRotation.x * Mathf.PI) / 180f, (degreesRotation.y * Mathf.PI) / 180f , (degreesRotation.z * Mathf.PI) / 180f);
        //"b0 x y z matrix  MATHx9"
        //about z
        /*
         *  | cos( z ) , -sin( z ),  0  |
         *  | sin( z ) ,  cos( z ),  0  |
         *  |     0    ,     0    ,  1  |
         */
        float[,] zMatrix = new float[3,3];
        zMatrix[0, 0] = Cos(rotation.z); zMatrix[0, 1] = -1 * Sin(rotation.z) ; zMatrix[0, 2] = 0;
        zMatrix[1, 0] = Sin(rotation.z); zMatrix[1, 1] = Cos(rotation.z)      ; zMatrix[1, 2] = 0;
        zMatrix[2, 0] = 0              ; zMatrix[2, 1] = 0                    ; zMatrix[2, 2] = 1;


        //about y
        /*
         *  |  cos( y ) ,  0  , sin ( y ) |
         *  |     0     ,  1  ,     0     |
         *  | -sin( y ) ,  0  , cos ( y ) |
         */
        float[,] yMatrix = new float[3, 3];
        yMatrix[0, 0] = Cos(rotation.y)      ; yMatrix[0, 1] = 0 ; yMatrix[0, 2] = Sin(rotation.y);
        yMatrix[1, 0] = 0                    ; yMatrix[1, 1] = 1 ; yMatrix[1, 2] = 0;
        yMatrix[2, 0] = -1 * Sin(rotation.y) ; yMatrix[2, 1] = 0 ; yMatrix[2, 2] = Cos(rotation.y);

        // about x 
        /*
         *  |   1 ,    0     ,    0      |
         *  |   0 , cos( x ) , -sin( x ) |
         *  |   0 , sin( x ) ,  cos( x ) |
         */
        float[,] xMatrix = new float[3, 3];
        xMatrix[0, 0] = 1 ; xMatrix[0, 1] = 0               ; xMatrix[0, 2] = 0                   ;
        xMatrix[1, 0] = 0 ; xMatrix[1, 1] = Cos(rotation.x) ; xMatrix[1, 2] = -1 * Sin(rotation.x);
        xMatrix[2, 0] = 0 ; xMatrix[2, 1] = Sin(rotation.x) ; xMatrix[2, 2] = Cos(rotation.x)     ;


        //Now we do some Matrix Multiplication.
        //Do zMatrix * yMatrix.. then the Result is multiplied with xMatrix and that is the solution
        
        float[,] pitchYawMatrix = MultiplyMatrix(xMatrix, yMatrix);
        float[,] pitchYawRollMatrix = MultiplyMatrix(pitchYawMatrix, zMatrix);

        configuration.Append(" matrix ");
        for (int i = 0; i < 3; i++ ) {
            for (int j = 0; j < 3; j++ ) {
                configuration.Append(pitchYawRollMatrix[i, j] + " ");
            }
        }


        //return configuration.ToString();
        Debug.Log(configuration.ToString());

    }

    private float Cos(float num) {
        return Mathf.Cos(num);
    }

    private float Sin(float num) {
        return Mathf.Sin(num);
    }

    float[,] MultiplyMatrix(float[,] a, float[,] b) {
        float[,] sol = new float[3,3];
        //not the issue
        sol[0, 0] = a[0, 0] * b[0, 0] + a[0, 1] * b[1, 0] + a[0, 2] * b[2, 0];
        sol[0, 1] = a[0, 0] * b[0, 1] + a[0, 1] * b[1, 1] + a[0, 2] * b[2, 1];
        sol[0, 2] = a[0, 0] * b[0, 2] + a[0, 1] * b[1, 2] + a[0, 2] * b[2, 2];
            sol[1, 0] = a[1, 0] * b[0, 0] + a[1, 1] * b[1, 0] + a[1, 2] * b[2, 0];
            sol[1, 1] = a[1, 0] * b[0, 1] + a[1, 1] * b[1, 1] + a[1, 2] * b[2, 1];
            sol[1, 2] = a[1, 0] * b[0, 2] + a[1, 1] * b[1, 2] + a[1, 2] * b[2, 2];
                sol[2, 0] = a[2, 0] * b[0, 0] + a[2, 1] * b[1, 0] + a[2, 2] * b[2, 0];
                sol[2, 1] = a[2, 0] * b[0, 1] + a[2, 1] * b[1, 1] + a[2, 2] * b[2, 1];
                sol[2, 2] = a[2, 0] * b[0, 2] + a[2, 1] * b[1, 2] + a[2, 2] * b[2, 2];
        return sol;
    } 
}
