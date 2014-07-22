using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace RhythmHell
{
    class Camera
    {
        //fields
        private Matrix4 rotationMatrix = Matrix4.Identity;
        private Vector3 cameraPos, cameraUp, cameraDir, cameraTarget;

        //constructors
        public Camera(Vector3 cameraPos, Vector3 cameraDir, Vector3 cameraUp)
        {
            this.cameraPos = cameraPos;
            this.cameraUp = cameraUp.Normalized();
            this.cameraDir = cameraDir.Normalized();
        }

        //properties
        public Matrix4 View
        {
            get { return Matrix4.LookAt(cameraPos, CameraTarget, cameraUp); }
        }

        public Matrix4 Projection
        {
            get;
            protected set;
        }

        public Vector3 CameraPos { get { return cameraPos; } }
        public Vector3 CameraUp { get { return cameraUp; } }
        public Vector3 CameraDir { get { return cameraDir; } }
        public Vector3 CameraRight { get { return Vector3.Cross(cameraDir, cameraUp).Normalized(); } }
        public Vector3 CameraTarget { get { return cameraTarget; } }

        //Public Methods
        public void UpdateTarget()
        {
            cameraDir = Vector3.TransformVector(cameraDir, rotationMatrix);
            cameraUp = Vector3.TransformVector(cameraUp, rotationMatrix);
            cameraTarget = Vector3.Add(cameraPos, cameraDir);

            rotationMatrix = Matrix4.Identity;  //reset rotation matrix
        }

        //Post: Moves camera
        public void MoveTo(Vector3 position)
        {
            cameraPos = position;
            rotationMatrix = Matrix4.Identity;
        }

        public void Move(Vector3 cameraMove)
        {
            cameraPos = Vector3.Add(cameraPos, cameraMove);
        }

        public void Roll(float angle)
        {
            rotationMatrix *= Matrix4.CreateFromAxisAngle(cameraDir, (float)MathHelper.ToRadians(angle));
        }

        public void Pitch(float angle)
        {
            rotationMatrix *= Matrix4.CreateFromAxisAngle(CameraRight, (float)MathHelper.ToRadians(angle));
        }

        public void Yaw(float angle)
        {
            rotationMatrix *= Matrix4.CreateFromAxisAngle(cameraUp, (float)MathHelper.ToRadians(angle));
        }
    }
}
