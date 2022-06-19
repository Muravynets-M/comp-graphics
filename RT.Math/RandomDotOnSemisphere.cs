using RT.Math.LinearAlgebra;

namespace RT.Math;

public class RandomDotOnSemisphere
{
    private Random _random;
    
     public RandomDotOnSemisphere()
        {
            _random = Random.Shared;
        }

     public Vector3 NextVector3(Vector3 polarVector)
     {
         return GenerateHemisphereDirection(polarVector);
     }
     
    private Vector3 GenerateHemisphereDirection(Vector3 polarVector)
        {
            var randDir = GenerateRandomDirectionOnSphere();
            
            var dotProd = Vector3.Dot(randDir, polarVector);
            while (dotProd < 0)
            {
                // try to generate again
                randDir = GenerateHemisphereDirection(polarVector);
                dotProd = Vector3.Dot(randDir, polarVector);
            }
    
            return Vector3.Unit(randDir);
        }
    
     private Vector3 GenerateRandomDirectionOnSphere()
        {
            var random = new Random();
            var u = random.NextSingle();
            var v = random.NextSingle();
    
            var theta = 2 * MathF.PI * u;
            var phi = 1 / MathF.Cos(2 * v - 1);
    
            var x = MathF.Sqrt(1 - u * u) * MathF.Cos(theta);
            var y = MathF.Sqrt(1 - u * u) * MathF.Sin(theta);
            var z = MathF.Cos(phi);
    
            return new Vector3(x, y, z);
        }
}