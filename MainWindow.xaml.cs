//*****************************************************************************
//** Cube 3D                                                                 **
//** A small project to build a 3D cube you can control with the keyboard.   **
//*****************************************************************************


using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace Cube3D
{
    public partial class MainWindow : Window
    {
        private double rotationX = 0;
        private double rotationY = 0;

        public MainWindow()
        {
            InitializeComponent();
            CreateCube();
            CompositionTarget.Rendering += OnRendering;
        }

        private void CreateCube()
        {
            var cube = new Model3DGroup();

            // Create cube faces with different colors
            cube.Children.Add(CreateFace(Colors.Red, new Vector3D(1, 0, 0), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1)));
            cube.Children.Add(CreateFace(Colors.Green, new Vector3D(-1, 0, 0), new Vector3D(0, 1, 0), new Vector3D(0, 0, 1)));
            cube.Children.Add(CreateFace(Colors.Blue, new Vector3D(0, 1, 0), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1)));
            cube.Children.Add(CreateFace(Colors.Yellow, new Vector3D(0, -1, 0), new Vector3D(1, 0, 0), new Vector3D(0, 0, 1)));
            cube.Children.Add(CreateFace(Colors.Cyan, new Vector3D(0, 0, 1), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0)));
            cube.Children.Add(CreateFace(Colors.Magenta, new Vector3D(0, 0, -1), new Vector3D(1, 0, 0), new Vector3D(0, 1, 0)));

            modelVisual.Content = cube;
        }

        private GeometryModel3D CreateFace(Color color, Vector3D normal, Vector3D up, Vector3D right)
        {
            var mesh = new MeshGeometry3D();
            mesh.Positions.Add(new Point3D(1, 1, 1)); // Top Right Front
            mesh.Positions.Add(new Point3D(-1, 1, 1)); // Top Left Front
            mesh.Positions.Add(new Point3D(-1, -1, 1)); // Bottom Left Front
            mesh.Positions.Add(new Point3D(1, -1, 1)); // Bottom Right Front

            mesh.Positions.Add(new Point3D(1, 1, -1)); // Top Right Back
            mesh.Positions.Add(new Point3D(-1, 1, -1)); // Top Left Back
            mesh.Positions.Add(new Point3D(-1, -1, -1)); // Bottom Left Back
            mesh.Positions.Add(new Point3D(1, -1, -1)); // Bottom Right Back

            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(7);

            var material = new DiffuseMaterial(new SolidColorBrush(color));
            return new GeometryModel3D(mesh, material);
        }

        private void OnRendering(object? sender, EventArgs e)
        {
            RotateCube();
        }

        private void RotateCube()
        {
            RotateTransform3D rotateX = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), rotationX));
            RotateTransform3D rotateY = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), rotationY));

            var transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(rotateY);
            transformGroup.Children.Add(rotateX);

            modelVisual.Transform = transformGroup;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            const double rotationStep = 5;

            if (e.Key == Key.Up)
            {
                rotationX += rotationStep;
            }
            else if (e.Key == Key.Down)
            {
                rotationX -= rotationStep;
            }
            else if (e.Key == Key.Left)
            {
                rotationY -= rotationStep;
            }
            else if (e.Key == Key.Right)
            {
                rotationY += rotationStep;
            }
        }
    }


}