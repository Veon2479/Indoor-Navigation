package com.example.client_ins;

import static java.lang.Math.sqrt;

import android.os.Build;

import androidx.annotation.RequiresApi;

enum MatrixType{
    ALL_ZERO,
    IDENTITY, //Единичная матрица
    UNDEFINED
}

class Matrix{
    public double[][] matrix;
    public int a;
    public int b;

    public Matrix(int a, int b, MatrixType type){
        matrix = new double[a][b];
        this.a = a;
        this.b = b;

        switch (type){
            case ALL_ZERO:
                for(int i = 0; i<a; i++)
                {
                    for(int j = 0; j<b; j++)
                    {
                        matrix[i][j] = 0;
                    }
                }
                break;
            case IDENTITY:
                for(int i = 0; i<a; i++)
                {
                    for(int j = 0; j<b; j++)
                    {
                        if(i == j)
                            matrix[i][j] = 1;
                        else
                            matrix[i][j] = 0;
                    }
                }
                break;
            default:
        }
    }

    public void Copy(Matrix A){
        if(a != A.a || b != A.b)
            return;

        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                matrix[i][j] = A.matrix[i][j];
            }
        }
    }

    public void Minor(Matrix A, int r, int c){
        if(a != A.a - 1 || b != A.b - 1 || r >= A.a || r < 0 || c >= A.b || c < 0)
            return;

        for(int i = 0; i<A.a; i++)
        {
            for(int j = 0; j<A.b; j++)
            {
                if(i != r && j != c){
                    int n = i<r?i:i-1;
                    int k = j<c?j:j-1;
                    matrix[n][k] = A.matrix[i][j];
                }
            }
        }
    }

    public void Scale(double x){
        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                matrix[i][j] = x*matrix[i][j];
            }
        }
    }

    public Matrix AlgAddMatrix(){
        Matrix R = new Matrix(a, b, MatrixType.UNDEFINED);

        Matrix temp = new Matrix(a - 1, a - 1, MatrixType.UNDEFINED);
        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                temp.Minor(this, i, j);
                double x = (2+j+i) % 2 == 0?1:-1;
                x *= temp.Determinant();
                R.matrix[i][j] = x;
            }
        }

        return R;
    }

    public double Determinant(){
        if(a != b)
            return 0;

        if(a == 2){
            return matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
        }
        else
        {
            double sum = 0;
            Matrix temp = new Matrix(a - 1, a - 1, MatrixType.UNDEFINED);
            for(int i = 0; i<a; i++)
            {
                temp.Minor(this, i, 0);

                int x = (2+i) % 2 == 0?1:-1;
                sum += x * temp.Determinant() * matrix[i][0];
            }
            return sum;
        }
    }

    public Matrix Transpose(){
        Matrix R = new Matrix(b, a, MatrixType.UNDEFINED);

        for(int i = 0; i<R.a; i++)
        {
            for(int j = 0; j<R.b; j++)
            {
                R.matrix[i][j] = matrix[j][i];
            }
        }

        return R;
    }

    public Matrix Invert(){
        Matrix R = this.AlgAddMatrix();
        R = R.Transpose();
        double x = 1/this.Determinant();
        R.Scale(x);
        return R;
    }

    public static Matrix Mul(Matrix A, Matrix B){
        if(A.b != B.a)
            return null;

        Matrix R = new Matrix(A.a, B.b, MatrixType.UNDEFINED);

        for(int i = 0; i<A.a; i++)
        {
            for(int j = 0; j<B.b; j++)
            {
                double sum = 0;

                for(int k = 0; k<A.b; k++)
                {
                    sum += A.matrix[i][k]*B.matrix[k][j];
                }

                R.matrix[i][j] = sum;
            }
        }
        return R;
    }

    public Matrix Add(Matrix A){
        if(a != A.a || b != A.b)
            return this;

        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                matrix[i][j] += A.matrix[i][j];
            }
        }
        return this;
    }

    public Matrix Sub(Matrix A){
        if(a != A.a || b != A.b)
            return this;

        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                matrix[i][j] -= A.matrix[i][j];
            }
        }
        return this;
    }

    public static Matrix Sub(Matrix A, Matrix B){
        if(B.a != A.a || B.b != A.b)
            return null;

        Matrix R = new Matrix(A.a, A.b, MatrixType.UNDEFINED);

        for(int i = 0; i<A.a; i++)
        {
            for(int j = 0; j<A.b; j++)
            {
                R.matrix[i][j] = A.matrix[i][j] - B.matrix[i][j];
            }
        }

        return R;
    }

    public static Matrix Add(Matrix A, Matrix B){
        if(B.a != A.a || B.b != A.b)
            return null;

        Matrix R = new Matrix(A.a, A.b, MatrixType.UNDEFINED);

        for(int i = 0; i<A.a; i++)
        {
            for(int j = 0; j<A.b; j++)
            {
                R.matrix[i][j] = A.matrix[i][j] + B.matrix[i][j];
            }
        }

        return R;
    }

    public void WriteMatrix(String name){

        String res = name + "=\n|";
        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                res += String.format("%.2f  ", matrix[i][j]);
            }
            res += "|\n|";
        }
        System.out.println(res);
    }

    public Matrix Sqrt(){
        if(a != b)
            return null;
        Matrix R = new Matrix(a, a, MatrixType.IDENTITY);

        double[] D = new double[this.a];

        for(int i = 0; i<a; i++)
        {
            double sum;

            for(int j = 0; j<=i; j++)
            {
                if(i>j){
                    sum = 0;
                    for(int k = 0; k < j; k++){
                        sum += R.matrix[i][k] * R.matrix[j][k] * D[k];
                    }
                    R.matrix[i][j] = (matrix[i][j] - sum)/D[j];
                }
            }
            sum = 0;
            for(int k = 0; k < i; k++){
                sum += R.matrix[i][k] * R.matrix[i][k] * D[k];
            }
            D[i] = matrix[i][i] - sum;
        }

        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
            {
                R.matrix[i][j] *= Math.sqrt(D[j]);
            }
        }

        return R;
    }
}

class Quaternion{
    public double x;
    public double y;
    public double z;
    public double w;

    public double Norm(){
        return x*x + y*y + z*z + w*w;
    }

    public void Normalize(){
        double l = Math.sqrt(x*x + y*y + z*z + w*w);
        this.x /= l;
        this.y /= l;
        this.z /= l;
        this.w /= l;
    }

    public void Mul(Quaternion q){
        double tx = w*q.x + x*q.w + y*q.z - z*q.y;
        double ty = w*q.y - x*q.z + y*q.w + z*q.x;
        double tz = w*q.z + x*q.y - y*q.x + z*q.w;
        double tw = w*q.w - x*q.x - y*q.y - z*q.z;

        this.x = tx;
        this.y = ty;
        this.z = tz;
        this.w = tw;
    }

    public void MulWithInverted(Quaternion q){
        double l = q.Norm();

        double tx = (- w*q.x + x*q.w - y*q.z + z*q.y) / l;
        double ty = (- w*q.y + x*q.z + y*q.w - z*q.x) / l;
        double tz = (- w*q.z - x*q.y + y*q.x + z*q.w) / l;
        double tw = (w*q.w + x*q.x + y*q.y + z*q.z) / l;

        this.x = tx;
        this.y = ty;
        this.z = tz;
        this.w = tw;
    }

    public void MulVec(Quaternion q){
        double tx = w*q.x + y*q.z - z*q.y;
        double ty = w*q.y - x*q.z + z*q.x;
        double tz = w*q.z + x*q.y - y*q.x;
        double tw = - x*q.x - y*q.y - z*q.z;

        this.x = tx;
        this.y = ty;
        this.z = tz;
        this.w = tw;
    }

    public void Invert(){
        double l = Norm();

        this.x = -x / l;
        this.y = -y / l;
        this.z = -z / l;
        this.w /= l;
    }
}

public class ClientMath implements Runnable{

    boolean isActive = true;
    boolean fPause = true;
    public boolean fPhysics = false;
    Engine mainEngine;

    public double initLatitude = 0;
    public double initLongitude = 0;

    final static double RADIUS_MAJOR = 6378137.0;
    final static double RADIUS_MINOR = 6356752.3142;
    private double Radius = 0;

    public double Latitude = 0;
    public double Longitude = 0;
    public double itudeAccur = 0.1f;

    public ClientMath(Engine engine) {
        this.mainEngine = engine;
        linAccQuat.w = 0;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void TransformCoordinates(){
        double angle = Math.toRadians(mainEngine.Azimuth);
        mainEngine.Crd1 = mainEngine.ReceivedCrd1 + currX.matrix[0][0] * Math.cos(angle) - currX.matrix[3][0] * Math.sin(angle);
        mainEngine.Crd2 = mainEngine.receivedCrd2 - (currX.matrix[0][0] * Math.sin(angle) + currX.matrix[3][0] * Math.cos(angle));
    }

    public void CorrectInitCoordinates(){
        double x = Math.cos(Math.toRadians(initLongitude));
        x *= x;
        x /= RADIUS_MAJOR * RADIUS_MAJOR;
        double y = Math.sin(Math.toRadians(initLongitude));
        y *= y;
        y /= RADIUS_MINOR * RADIUS_MINOR;
        Radius = Math.sqrt(1/(x+y));
    }

    public void CorrectCoordinates(){
        GPSX = Math.toRadians(Latitude - initLatitude) * Radius;
        GPSY = Math.toRadians(Longitude - initLongitude) * Radius;
    }

    public Quaternion tempQuat = new Quaternion();
    public Quaternion rotQuat = new Quaternion();

    public Quaternion linAccQuat = new Quaternion();

    private double time;
    private double deltaTime;

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void UpdateGlobalAcc()
    {
        tempQuat.x = rotQuat.x;
        tempQuat.y = rotQuat.y;
        tempQuat.z = rotQuat.z;
        tempQuat.w = rotQuat.w;

        tempQuat.Mul(linAccQuat);
        tempQuat.MulWithInverted(rotQuat);

        z.matrix[2][0] = tempQuat.x;
        z.matrix[3][0] = tempQuat.y;
        mainEngine.accX = tempQuat.x;
        mainEngine.accY = tempQuat.y;
    }

/*

*/
    private Matrix H = new Matrix(4,6, MatrixType.ALL_ZERO); // Observation matrix
    public Matrix P = new Matrix(6, 6, MatrixType.ALL_ZERO); // Covariance Matrix
    private Matrix P1  = new Matrix(6,6, MatrixType.UNDEFINED); // Copy of covariance matrix
    private Matrix F = new Matrix(6,6, MatrixType.IDENTITY); // State transition matrix
    private Matrix R = new Matrix(4, 4, MatrixType.ALL_ZERO);// Measurement covariance matrix
    private Matrix Q = new Matrix(6,6, MatrixType.ALL_ZERO);// Process Noise Matrix
    private Matrix K; // Kalman Gain

    private Matrix predX = new Matrix(6, 1, MatrixType.ALL_ZERO); //Predicted system state
    private Matrix tempX;
    public Matrix currX = new Matrix(6, 1, MatrixType.ALL_ZERO); //Estimated system state

    public Matrix z = new Matrix(4, 1, MatrixType.ALL_ZERO); // Measurement matrix
    private Matrix zt = new Matrix(z.a, z.b, MatrixType.UNDEFINED);// Mean optimal prediction
    private Matrix tempZ = new Matrix(z.a, z.b, MatrixType.UNDEFINED);

    private double GPSX = 0;
    private double GPSY = 0;

    private int L = 6;                                          //Number of dimensions
    private int k = 1;                                          //Used to calculate the weighs
    private double alpha = 0.001f;                              //Used to calculate the weighs
    private int beta = 2;                                       //Used to calculate the weighs
    private double u = alpha*alpha*(L + k) - L;                 //Used to calculate the weighs and sigma points
    private double wm0 = u/(L + u);                             //Weighs
    private double wc0 = u/(L + u)+ (1 - alpha*alpha + beta);   //Weighs
    private double wm = 1 / (2*(L + u));                        //Weighs
    private double wc = wm;                                     //Weighs
    private Matrix[] sigmaPoints = new Matrix[2*L+1];
    private Matrix[] Zt = new Matrix[2*L+1];                    //Optimal prediction sigma points
    private Matrix St;                                          //Used to calculate K
    private Matrix Pxz;                                         //Used to calculate K

    private double dispA = 1;           //Dispersion of acceleration random changes
    private double dispX = 2;           //Dispersion of coordinates random changes
    private double errorA = 0.16f;      //Error of measurement of acceleration

    void KalmanFilter(){
        Q.matrix[2][2] = 1;
        Q.matrix[5][5] = 1;
        Q.matrix[2][1] = deltaTime;
        Q.matrix[1][2] = deltaTime;
        Q.matrix[5][4] = deltaTime;          // Q = Qa + Qv + Qx
        Q.matrix[4][5] = deltaTime;
        double tempT = deltaTime * deltaTime;// Qv = 0;
        Q.matrix[1][1] = tempT;
        Q.matrix[4][4] = tempT;              //     |dt^4 / 4   dt^3 / 2   dt^2 / 2   0         0         0      |
        tempT /= 2;                          //     |dt^3 / 2   dt^2       dt         0         0         0      |
        Q.matrix[0][2] = tempT;              //Qa = |dt^2 / 2   dt         1          0         0         0      | * disp(a)^2
        Q.matrix[2][0] = tempT;              //     |0          0          0       dt^4 / 4   dt^3 / 2   dt^2 / 2|
        Q.matrix[3][5] = tempT;              //     |0          0          0       dt^3 / 2   dt^2       dt      |
        Q.matrix[5][3] = tempT;              //     |0          0          0       dt^2 / 2   dt          1      |
        tempT *= deltaTime/2;
        Q.matrix[0][1] = tempT;              //     |1   0   0   0   0   0|
        Q.matrix[1][0] = tempT;              //     |0   0   0   0   0   0|
        Q.matrix[3][4] = tempT;              //Qx = |0   0   0   0   0   0| * disp(x)^2
        Q.matrix[4][3] = tempT;              //     |0   0   0   1   0   0|
        tempT *= deltaTime/2;                //     |0   0   0   0   0   0|
        Q.matrix[0][0] = tempT + 2;          //     |0   0   0   0   0   0|
        Q.matrix[3][3] = tempT + 2;
        Q.Scale(dispA);
        Q.matrix[0][0] += dispX;
        Q.matrix[3][3] += dispX;
        tempT = deltaTime*deltaTime/2;
        F.matrix[0][2] = tempT;     //     |1   dt  0.5dt^2  0   0   0      |
        F.matrix[3][5] = tempT;     //     |0   1   dt       0   0   0      |
        F.matrix[0][1] = deltaTime; // F = |0   0   1        0   0   0      |
        F.matrix[1][2] = deltaTime; //     |0   0   0        1   dt  0.5dt^2|
        F.matrix[3][4] = deltaTime; //     |0   0   0        0   1   dt     |
        F.matrix[4][5] = deltaTime; //     |0   0   0        0   0   1      |

        R.matrix[0][0] = itudeAccur; //Error of measurement of GPS
        R.matrix[1][1] = itudeAccur;

        sigmaPoints[0].Copy(currX);
        P1.Copy(P);
        P1 = P1.Sqrt();
        P1.Scale(Math.sqrt(L+u));
        for(int i = 1; i<=L; i++){
            for(int j = 0; j<sigmaPoints[i].a; j++)
                sigmaPoints[i].matrix[j][0] = sigmaPoints[0].matrix[j][0] + P1.matrix[j][i-1];
        }
        for(int i = L+1; i<=2*L; i++){
            for(int j = 0; j<sigmaPoints[i].a; j++)
                sigmaPoints[i].matrix[j][0] = sigmaPoints[0].matrix[j][0] - P1.matrix[j][i-L-1];
        }

        predX = Matrix.Mul(F, sigmaPoints[0]);
        predX.Scale(wm0);
        for(int i = 1; i<=2*L; i++){
            Matrix temp = Matrix.Mul(F, sigmaPoints[i]);
            temp.Scale(wm);
            predX.Add(temp);
        }

        tempX = Matrix.Mul(F, sigmaPoints[0]).Sub(predX);
        P = Matrix.Mul(tempX, tempX.Transpose());
        P.Scale(wc0);
        for(int i = 1; i<2*L + 1; i++){
            tempX = Matrix.Mul(F, sigmaPoints[i]).Sub(predX);
            P1 = Matrix.Mul(tempX, tempX.Transpose());
            P1.Scale(wc);
            P.Add(P1);
        }
        P.Add(Q);

        sigmaPoints[0].Copy(predX);
        P1.Copy(P);
        P1 = P1.Sqrt();
        P1.Scale(Math.sqrt(L+u));
        for(int i = 1; i<=L; i++){
            for(int j = 0; j<sigmaPoints[i].a; j++)
                sigmaPoints[i].matrix[j][0] = sigmaPoints[0].matrix[j][0] + P1.matrix[j][i-1];
        }
        for(int i = L+1; i<=2*L; i++){
            for(int j = 0; j<sigmaPoints[i].a; j++)
                sigmaPoints[i].matrix[j][0] = sigmaPoints[0].matrix[j][0] - P1.matrix[j][i-L-1];
        }

        for(int i = 0; i<=2*L; i++){
            Zt[i] = Matrix.Mul(H, sigmaPoints[i]);
        }

        zt.Copy(Zt[0]);
        zt.Scale(wm0);
        for(int i = 1; i<=2*L; i++){
            tempZ.Copy(Zt[i]);
            tempZ.Scale(wm);
            zt.Add(tempZ);
        }

        Matrix temp = Matrix.Sub(Zt[0], zt);
        St = Matrix.Mul(temp, temp.Transpose());
        St.Scale(wc0);
        for(int i = 1; i<=2*L; i++){
            temp = Matrix.Sub(Zt[i], zt);
            temp = Matrix.Mul(temp, temp.Transpose());
            temp.Scale(wc);
            St.Add(temp);
        }
        St.Add(R);

        Pxz = Matrix.Mul(Matrix.Sub(sigmaPoints[0], predX), (Matrix.Sub(Zt[0], zt)).Transpose());
        Pxz.Scale(wc0);
        for(int i = 1; i<=2*L; i++){
            temp = Matrix.Mul(Matrix.Sub(sigmaPoints[i], predX), (Matrix.Sub(Zt[i], zt)).Transpose());
            temp.Scale(wc);
            Pxz.Add(temp);
        }

        temp = St.Invert();
        K = Matrix.Mul(Pxz, temp);

        currX = Matrix.Add(predX, Matrix.Mul(K, z.Sub(zt)));

        P = P.Sub(Matrix.Mul(Matrix.Mul(K, St), K.Transpose()));
    }

    public void Disable(){
        isActive = false;
    }

    public void Pause(){
        fPause = true;
    }

    public void Unpause(){
        time = System.nanoTime();
        fPause = false;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void EstimateMeasuredCoords(){
        z.matrix[0][0] = GPSX + mainEngine.WiFiCrd1 / 2;
        z.matrix[1][0] = GPSY + mainEngine.WiFiCrd2 / 2;
    }

    public void UpdateMeasuredCoords(){
        z.matrix[0][0] = GPSX;
        z.matrix[1][0] = GPSY;
    }

    @RequiresApi(api = Build.VERSION_CODES.O)
    public void run()
    {
        H.matrix[0][0] = 1;//       |1 0 0 0 0 0|
        H.matrix[1][3] = 1;//   H = |0 0 0 1 0 0|
        H.matrix[2][2] = 1;//       |0 0 1 0 0 0|
        H.matrix[3][5] = 1;//       |0 0 0 0 0 1|

        /*
        P.matrix[0][0] = 0.1f; P.matrix[0][1] = 0.01f; P.matrix[0][2] = 0.01f;
        P.matrix[1][0] = 0.01f; P.matrix[1][1] = 0.1f; P.matrix[1][2] = 0.01f;
        P.matrix[2][0] = 0.01f; P.matrix[2][1] = 0.01f; P.matrix[2][2] = 0.1f;
        P.matrix[3][0] = 0.01f; P.matrix[3][1] = 0.01f; P.matrix[3][2] = 0.01f;
        P.matrix[4][0] = 0.01f; P.matrix[4][1] = 0.01f; P.matrix[4][2] = 0.01f;
        P.matrix[5][0] = 0.01f; P.matrix[5][1] = 0.01f; P.matrix[5][2] = 0.01f;
        P.matrix[0][3] = 0.01f; P.matrix[0][4] = 0.01f; P.matrix[0][5] = 0.01f;
        P.matrix[1][3] = 0.01f; P.matrix[1][4] = 0.01f; P.matrix[1][5] = 0.01f;
        P.matrix[2][3] = 0.01f; P.matrix[2][4] = 0.01f; P.matrix[2][5] = 0.01f;
        P.matrix[3][3] = 0.1f; P.matrix[3][4] = 0.01f; P.matrix[3][5] = 0.01f;
        P.matrix[4][3] = 0.01f; P.matrix[4][4] = 0.1f; P.matrix[4][5] = 0.01f;
        P.matrix[5][3] = 0.01f; P.matrix[5][4] = 0.01f; P.matrix[5][5] = 0.1f;
         */

        P.matrix[0][0] = 0.1f;//        |0.1  0   0   0   0   0 |
        P.matrix[1][1] = 0.1f;//        | 0  0.1  0   0   0   0 |
        P.matrix[2][2] = 0.1f;//   P0 = | 0   0  0.1  0   0   0 |
        P.matrix[3][3] = 0.1f;//        | 0   0   0  0.1  0   0 |
        P.matrix[4][4] = 0.1f;//        | 0   0   0   0  0.1  0 |
        P.matrix[5][5] = 0.1f;//        | 0   0   0   0   0  0.1|

                                //      | ox   0    0    0  |
                                //  R = | 0    oy   0    0  |
        R.matrix[2][2] = errorA;//      | 0    0   oax   0  | oax = const
        R.matrix[3][3] = errorA;//      | 0    0    0   oay | oay = const

        for(int i = 0; i<2*L + 1; i++){
            Zt[i] = new Matrix(z.a, z.b, MatrixType.ALL_ZERO);
            sigmaPoints[i] = new Matrix(currX.a, currX.b, MatrixType.ALL_ZERO);
        }

        time = System.nanoTime();
        while(isActive)
        {
            if(!fPause){
                deltaTime = ((float)System.nanoTime()) / 1000000000 - time;
                if(deltaTime < 0){
                    time += deltaTime;
                }
                else if(deltaTime > 0.00001){
                    if(fPhysics) {
                        time += deltaTime;
                        if(mainEngine.isWiFiDetermined)
                            EstimateMeasuredCoords();
                        else
                            UpdateMeasuredCoords();
                        KalmanFilter();
                        TransformCoordinates();
                        fPhysics = false;
                    }
                }
            }
        }
    }
}
