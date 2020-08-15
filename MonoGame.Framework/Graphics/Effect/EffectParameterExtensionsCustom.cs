using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Xna.Framework.Graphics
{
    // TODO: System.Numerics Get() functions.
    public partial class EffectParameter
    {
        public void SetValue (System.Numerics.Vector2 value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float[])Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue (System.Numerics.Vector2[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue (value[i]);
            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue (System.Numerics.Vector3 value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float[])Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            fData[2] = value.Z;
            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue (System.Numerics.Vector3[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue (value[i]);
            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue (System.Numerics.Vector4 value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float[])Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            fData[2] = value.Z;
            fData[3] = value.W;
            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue (System.Numerics.Vector4[] value)
        {
            for (var i = 0; i < value.Length; i++)
                Elements[i].SetValue (value[i]);
            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue(System.Numerics.Matrix4x4 value)
        {
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            // HLSL expects matrices to be transposed by default.
            // These unrolled loops do the transpose during assignment.
            if (RowCount == 4 && ColumnCount == 4)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;
                fData[3] = value.M41;

                fData[4] = value.M12;
                fData[5] = value.M22;
                fData[6] = value.M32;
                fData[7] = value.M42;

                fData[8] = value.M13;
                fData[9] = value.M23;
                fData[10] = value.M33;
                fData[11] = value.M43;

                fData[12] = value.M14;
                fData[13] = value.M24;
                fData[14] = value.M34;
                fData[15] = value.M44;
            }
            else if (RowCount == 4 && ColumnCount == 3)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;
                fData[3] = value.M41;

                fData[4] = value.M12;
                fData[5] = value.M22;
                fData[6] = value.M32;
                fData[7] = value.M42;

                fData[8] = value.M13;
                fData[9] = value.M23;
                fData[10] = value.M33;
                fData[11] = value.M43;
            }
            else if (RowCount == 3 && ColumnCount == 4)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;

                fData[3] = value.M12;
                fData[4] = value.M22;
                fData[5] = value.M32;

                fData[6] = value.M13;
                fData[7] = value.M23;
                fData[8] = value.M33;

                fData[9] = value.M14;
                fData[10] = value.M24;
                fData[11] = value.M34;
            }
            else if (RowCount == 3 && ColumnCount == 3)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;

                fData[3] = value.M12;
                fData[4] = value.M22;
                fData[5] = value.M32;

                fData[6] = value.M13;
                fData[7] = value.M23;
                fData[8] = value.M33;
            }
            else if (RowCount == 3 && ColumnCount == 2)
            {
                var fData = (float[])Data;

                fData[0] = value.M11;
                fData[1] = value.M21;
                fData[2] = value.M31;

                fData[3] = value.M12;
                fData[4] = value.M22;
                fData[5] = value.M32;
            }

            StateKey = unchecked(NextStateKey++);
        }

        public void SetValue (System.Numerics.Matrix4x4[] value)
		{
            if (ParameterClass != EffectParameterClass.Matrix || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

		    if (RowCount == 4 && ColumnCount == 4)
		    {
		        for (var i = 0; i < value.Length; i++)
		        {
		            var fData = (float[])Elements[i].Data;

		            fData[0] = value[i].M11;
		            fData[1] = value[i].M21;
		            fData[2] = value[i].M31;
		            fData[3] = value[i].M41;

		            fData[4] = value[i].M12;
		            fData[5] = value[i].M22;
		            fData[6] = value[i].M32;
		            fData[7] = value[i].M42;

		            fData[8] = value[i].M13;
		            fData[9] = value[i].M23;
		            fData[10] = value[i].M33;
		            fData[11] = value[i].M43;

		            fData[12] = value[i].M14;
		            fData[13] = value[i].M24;
		            fData[14] = value[i].M34;
		            fData[15] = value[i].M44;
		        }
		    }
		    else if (RowCount == 4 && ColumnCount == 3)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float[])Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;
                    fData[3] = value[i].M41;

                    fData[4] = value[i].M12;
                    fData[5] = value[i].M22;
                    fData[6] = value[i].M32;
                    fData[7] = value[i].M42;

                    fData[8] = value[i].M13;
                    fData[9] = value[i].M23;
                    fData[10] = value[i].M33;
                    fData[11] = value[i].M43;
                }
            }
            else if (RowCount == 3 && ColumnCount == 4)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float[])Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;

                    fData[3] = value[i].M12;
                    fData[4] = value[i].M22;
                    fData[5] = value[i].M32;

                    fData[6] = value[i].M13;
                    fData[7] = value[i].M23;
                    fData[8] = value[i].M33;

                    fData[9] = value[i].M14;
                    fData[10] = value[i].M24;
                    fData[11] = value[i].M34;
                }
            }
            else if (RowCount == 3 && ColumnCount == 3)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float[])Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;

                    fData[3] = value[i].M12;
                    fData[4] = value[i].M22;
                    fData[5] = value[i].M32;

                    fData[6] = value[i].M13;
                    fData[7] = value[i].M23;
                    fData[8] = value[i].M33;
                }
            }
            else if (RowCount == 3 && ColumnCount == 2)
            {
                for (var i = 0; i < value.Length; i++)
                {
                    var fData = (float[])Elements[i].Data;

                    fData[0] = value[i].M11;
                    fData[1] = value[i].M21;
                    fData[2] = value[i].M31;

                    fData[3] = value[i].M12;
                    fData[4] = value[i].M22;
                    fData[5] = value[i].M32;
                }
            }

            StateKey = unchecked(NextStateKey++);
		}

        public void SetValue (System.Numerics.Quaternion value)
        {
            if (ParameterClass != EffectParameterClass.Vector || ParameterType != EffectParameterType.Single)
                throw new InvalidCastException();

            var fData = (float[])Data;
            fData[0] = value.X;
            fData[1] = value.Y;
            fData[2] = value.Z;
            fData[3] = value.W;
            StateKey = unchecked(NextStateKey++);
        }
    }
}
