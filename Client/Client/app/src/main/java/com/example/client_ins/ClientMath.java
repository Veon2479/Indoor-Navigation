package com.example.client_ins;

import static java.lang.Math.sqrt;

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

        for(int i = 0; i<a; i++)
        {
            for(int j = 0; j<b; j++)
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
        Matrix R = this.Transpose();
        R.Scale(1/this.Determinant());
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
    boolean fPause = false;
    public boolean fPhysics = true;
    Engine mainEngine;

    public ClientMath(Engine engine) {
        this.mainEngine = engine;
        linAccQuat.w = 0;
    }

    public Quaternion tempQuat = new Quaternion();
    public Quaternion rotQuat = new Quaternion();

    public Quaternion linAccQuat = new Quaternion();

    public double accX = 0;
    public double accY = 0;
    public double accZ = 0;

    public double velX = 0;
    public double velY = 0;

    private double time;
    private double deltaTime;

    private double dispA = 100.0f;

    public void UpdateGlobalAcc()
    {
        tempQuat.x = rotQuat.x;
        tempQuat.y = rotQuat.y;
        tempQuat.z = rotQuat.z;
        tempQuat.w = rotQuat.w;

        //tempQuat.Invert();
        //tempQuat.MulVec(linAccQuat);
        tempQuat.Mul(linAccQuat);
        tempQuat.MulWithInverted(rotQuat);

        //accX = tempQuat.x;
        //accY = tempQuat.y;
        z.matrix[0][0] = tempQuat.x;
        z.matrix[1][0] = tempQuat.y;
        mainEngine.accX = tempQuat.x;
        mainEngine.accY = tempQuat.y;
        //accZ = tempQuat.z;
    }

    void PhysicsProc(){
        mainEngine.Crd1 += velX * deltaTime + accX * deltaTime * deltaTime / 2;
        mainEngine.Crd2 += velY * deltaTime + accY * deltaTime * deltaTime / 2;

        velX += accX * deltaTime;
        velY += accY * deltaTime;
    }

/*
    x(n+1) = F * x(n)
        |1   t  0.5t  0   0   0  |
    F = |0   1   t    0   0   0  |
        |0   0   1    0   0   0  |
        |0   0   0    1   t  0.5t|
        |0   0   0    0   1   t  |
        |0   0   0    0   0   1  |
__________________________________
    p(n+1) = F*P*F.Transpose + Q
        |t^4 / 4   t^3 / 2   t^2 / 2   0         0         0      |
    Q = |t^3 / 2   t^2       t         0         0         0      |
        |t^2 / 2   t         1         0         0         0      | * disp(a)^2
        |0         0         0         t^4 / 4   t^3 / 2   t^2 / 2|
        |0         0         0         t^3 / 2   t^2       t      |
        |0         0         0         t^2 / 2   t         1      |
----------------------------------
   z = H * x + v
   x = |x | z = |ax|
       |vx|     |ay|
       |ax|
       |y |
       |vy|
       |ay|

  H = |0 0 1 0 0 0|
      |0 0 0 0 0 1|
----------------------------------
   K = P * H.Transpose * (H * P * H.Transpose + R)^-1
   R = |disp(ax) 0       |
       |0        disp(ay)|
----------------------------------
   x(n) = x(n-1) + K * (z - H * x(n-1))
----------------------------------
   P(n) = (I - K*H)*P(n-1) * (I - K*H).Transpose + K*R * K.Transpose
*/
    private Matrix H;
    private Matrix Htr;
    private Matrix P;
    private Matrix P1;
    private Matrix F;
    private Matrix R;
    private Matrix Q;
    private Matrix K;
    private Matrix I;

    private Matrix predX;
    private Matrix currX;

    private Matrix z;

    void KalmanFilter(){
        Q.matrix[2][2] = 1;
        Q.matrix[5][5] = 1;
        Q.matrix[2][1] = deltaTime;
        Q.matrix[1][2] = deltaTime;
        Q.matrix[5][4] = deltaTime;
        Q.matrix[4][5] = deltaTime;
        double tempT = deltaTime * deltaTime;
        Q.matrix[1][1] = tempT;
        Q.matrix[4][4] = tempT;
        tempT /= 2;
        Q.matrix[0][2] = tempT;
        Q.matrix[2][0] = tempT;
        Q.matrix[3][5] = tempT;
        Q.matrix[5][3] = tempT;
        tempT *= deltaTime/2;
        Q.matrix[0][1] = tempT;
        Q.matrix[1][0] = tempT;
        Q.matrix[3][4] = tempT;
        Q.matrix[4][3] = tempT;
        tempT *= deltaTime/2;
        Q.matrix[0][0] = tempT;
        Q.matrix[3][3] = tempT;
        Q.Scale(dispA);
        tempT = deltaTime/2;
        F.matrix[0][2] = tempT;
        F.matrix[3][5] = tempT;
        F.matrix[0][1] = deltaTime;
        F.matrix[1][2] = deltaTime;
        F.matrix[3][4] = deltaTime;
        F.matrix[4][5] = deltaTime;

        predX = Matrix.Mul(F, currX);

        P1.Copy(P);
        P = Matrix.Mul(Matrix.Mul(F, P1), F.Transpose()).Add(Q);

        K = Matrix.Mul(Matrix.Mul(P, Htr), (Matrix.Mul(Matrix.Mul(H, P), Htr).Add(R)).Invert());

        currX = predX.Add(Matrix.Mul(K, z.Sub(Matrix.Mul(H, predX))));

        Matrix temp = Matrix.Sub(I, Matrix.Mul(K, H));

        P1.Copy(P);
        P = Matrix.Mul(Matrix.Mul(temp, P1), temp.Transpose()).Add(Matrix.Mul(Matrix.Mul(K, R), K.Transpose()));
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

    public void run()
    {
        z = new Matrix(2, 1, MatrixType.ALL_ZERO);
        H = new Matrix(2, 6, MatrixType.ALL_ZERO);
        H.matrix[0][2] = 1;
        H.matrix[1][5] = 1;
        Htr = H.Transpose();
        P = new Matrix(6, 6, MatrixType.ALL_ZERO);
        P.matrix[0][0] = 0;
        P.matrix[1][1] = 0;
        P.matrix[2][2] = 500;
        P.matrix[3][3] = 0;
        P.matrix[4][4] = 0;
        P.matrix[5][5] = 500;
        P1 = new Matrix(6,6, MatrixType.UNDEFINED);
        F = new Matrix(6,6, MatrixType.IDENTITY);
        R = new Matrix(2, 2, MatrixType.ALL_ZERO);
        R.matrix[0][0] = 0.01;
        R.matrix[1][1] = 0.01;
        Q = new Matrix(6,6, MatrixType.ALL_ZERO);
        I = new Matrix(6, 6, MatrixType.IDENTITY);
        currX = new Matrix(6, 1, MatrixType.ALL_ZERO);
        predX = new Matrix(6, 1, MatrixType.ALL_ZERO);


        time = System.nanoTime();
        while(isActive)
        {
            if(!fPause){
                deltaTime = ((float)System.nanoTime()) / 1000000000 - time;
                //System.out.println(deltaTime);
                if(deltaTime < 0){
                    time += deltaTime;
                }
                else if(deltaTime > 0.00001){
                    if(fPhysics) {
                        time += deltaTime;
                        KalmanFilter();
                        //PhysicsProc();
                        mainEngine.Crd1 = currX.matrix[0][0];
                        mainEngine.Crd2 = currX.matrix[3][0];
                        fPhysics = false;
                    }
                }
            }
        }
    }
}
