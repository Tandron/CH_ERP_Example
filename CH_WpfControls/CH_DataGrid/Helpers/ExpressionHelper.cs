using System.Linq.Expressions;

namespace CH_WpfControls.CH_DataGrid.Helpers
{
    public class ExpressionHelper
    {
        public static MethodCallExpression ToString(Expression prop)
        {
            return Expression.Call(prop, typeof(object).GetMethod("ToString", System.Type.EmptyTypes));
        }

        public static MethodCallExpression ToLower(MethodCallExpression stringProp)
        {
            return Expression.Call(stringProp, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
        }

        public static BinaryExpression NotNull(Expression prop)
        {
            return Expression.NotEqual(prop, Expression.Constant(null));
        }

        public static Predicate<object> GenerateGeneric(MemberExpression prop, ConstantExpression val, Type type, ParameterExpression objParam, string methodName)
        {
            if (type != null)
            {
                var containsExpression = Expression.Call(ExpressionHelper.ToLower(ExpressionHelper.ToString(prop)), methodName, null, ExpressionHelper.ToLower(ExpressionHelper.ToString(val)));
                var exp = Expression.AndAlso(ExpressionHelper.NotNull(prop), containsExpression);
                Expression<Func<object, bool>> equalfunction = Expression.Lambda<Func<object, bool>>(exp, objParam);
                return new Predicate<object>(equalfunction.Compile());
            }
            else
            {
                var exp = Expression.Call(ExpressionHelper.ToLower(ExpressionHelper.ToString(prop)), methodName, null, ExpressionHelper.ToLower(ExpressionHelper.ToString(val)));
                Expression<Func<object, bool>> equalfunction = Expression.Lambda<Func<object, bool>>(exp, objParam);
                return new Predicate<object>(equalfunction.Compile());
            }
        }

        public static Predicate<object> GenerateEquals(MemberExpression prop, string value, Type type, ParameterExpression objParam)
        {
            BinaryExpression equalExpresion = null;
            if (type != null)
            {
                object equalTypedInput = ValueConvertor(type, value);
                if (equalTypedInput != null)
                {
                    var equalValue = Expression.Constant(equalTypedInput, type);
                    equalExpresion = Expression.Equal(prop, equalValue);
                }
            }
            else
            {
                var toStringExp = Expression.Equal(ToLower(ToString(prop)), ExpressionHelper.ToLower(ExpressionHelper.ToString(Expression.Constant(value))));
                if (type != typeof(DateTime))
                    equalExpresion = Expression.AndAlso(ExpressionHelper.NotNull(prop), toStringExp);
                else
                    equalExpresion = toStringExp;


            }
            if (equalExpresion != null)
            {
                Expression<Func<object, bool>> equalfunction = Expression.Lambda<Func<object, bool>>(equalExpresion, objParam);
                return new Predicate<object>(equalfunction.Compile());
            }
            else
                return null;
        }

        public static Predicate<object> GenerateNotEquals(MemberExpression prop, string value, Type type, ParameterExpression objParam)
        {
            BinaryExpression notEqualExpresion = null;
            if (type != null)
            {
                object equalTypedInput = ValueConvertor(type, value);
                if (equalTypedInput != null)
                {
                    var equalValue = Expression.Constant(equalTypedInput, type);
                    notEqualExpresion = Expression.NotEqual(prop, equalValue);
                }
            }
            else
            {
                var toStringExp = Expression.NotEqual(ToLower(ToString(prop)), ExpressionHelper.ToLower(ExpressionHelper.ToString(Expression.Constant(value))));
                notEqualExpresion = Expression.AndAlso(ExpressionHelper.NotNull(prop), toStringExp);

            }
            if (notEqualExpresion != null)
            {
                Expression<Func<object, bool>> equalfunction = Expression.Lambda<Func<object, bool>>(notEqualExpresion, objParam);
                return new Predicate<object>(equalfunction.Compile());
            }
            else
                return null;
        }

        public static Predicate<object> GenerateGreaterThanEqual(MemberExpression prop, string value, Type type, ParameterExpression objParam)
        {
            object typedInput = ValueConvertor(type, value);
            if (typedInput != null)
            {
                var greaterThanEqualValue = Expression.Constant(typedInput, type);
                var greaterThanEqualExpresion = Expression.GreaterThanOrEqual(prop, greaterThanEqualValue);
                Expression<Func<object, bool>> greaterThanEqualfunction = Expression.Lambda<Func<object, bool>>(greaterThanEqualExpresion, objParam);
                return new Predicate<object>(greaterThanEqualfunction.Compile());
            }
            else
            {
                return null;
            }
        }

        public static Predicate<object> GenerateLessThanEqual(MemberExpression prop, string value, Type type, ParameterExpression objParam)
        {
            object typedInput = ValueConvertor(type, value);
            if (typedInput != null)
            {
                var lessThanEqualValue = Expression.Constant(typedInput, type);
                var lessThanEqualExpresion = Expression.LessThanOrEqual(prop, lessThanEqualValue);
                Expression<Func<object, bool>> lessThanEqualfunction = Expression.Lambda<Func<object, bool>>(lessThanEqualExpresion, objParam);
                return new Predicate<object>(lessThanEqualfunction.Compile());
            }
            else
            {
                return null;
            }
        }
        public static Predicate<object> GenerateLessThan(MemberExpression prop, string value, Type type, ParameterExpression objParam)
        {
            object typedInput = ValueConvertor(type, value);
            if (typedInput != null)
            {
                var lessThan = Expression.Constant(typedInput, type);
                var lessThanExpresion = Expression.LessThan(prop, lessThan);
                Expression<Func<object, bool>> lessThanfunction = Expression.Lambda<Func<object, bool>>(lessThanExpresion, objParam);
                return new Predicate<object>(lessThanfunction.Compile());
            }
            else
            {
                return null;
            }
        }
        public static Predicate<object> GenerateGreaterThan(MemberExpression prop, string value, Type type, ParameterExpression objParam)
        {
            object typedInput = ValueConvertor(type, value);
            if (typedInput != null)
            {
                var greaterThanValue = Expression.Constant(typedInput, type);
                var greaterThanExpresion = Expression.GreaterThan(prop, greaterThanValue);
                Expression<Func<object, bool>> greaterThanfunction = Expression.Lambda<Func<object, bool>>(greaterThanExpresion, objParam);
                return new Predicate<object>(greaterThanfunction.Compile());
            }
            else
            {
                return null;
            }
        }

        public static Predicate<object> GenerateBetweenValues(MemberExpression prop, string value1, string value2, Type type, ParameterExpression objParam)
        {
            object typedInput1 = ValueConvertor(type, value1);
            Predicate<object> predicate1 = null;
            if (typedInput1 != null)
            {
                var greaterThanEqualValue = Expression.Constant(typedInput1, type);
                var greaterThanEqualExpression = Expression.GreaterThanOrEqual(prop, greaterThanEqualValue);
                Expression<Func<object, bool>> greaterThanEqualfunction = Expression.Lambda<Func<object, bool>>(greaterThanEqualExpression, objParam);
                predicate1 = new Predicate<object>(greaterThanEqualfunction.Compile());
            }
            object typedInput2 = ValueConvertor(type, value2);
            Predicate<object> predicate2 = null;

            if (typedInput2 != null)
            {
                var lessThanEqualValue = Expression.Constant(typedInput2, type);
                var lessThanEqualExpression = Expression.LessThanOrEqual(prop, lessThanEqualValue);
                Expression<Func<object, bool>> lessThanEqualfunction = Expression.Lambda<Func<object, bool>>(lessThanEqualExpression, objParam);
                predicate2 = new Predicate<object>(lessThanEqualfunction.Compile());

                return predicate1.And(predicate2);
            }
            else
            {
                return null;
            }
        }

        public static object ValueConvertor(Type type, string value)
        {

            if (type == typeof(byte) || type == typeof(byte?))
            {
                byte x;
                if (byte.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(sbyte) || type == typeof(sbyte?))
            {
                sbyte x;
                if (sbyte.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                short x;
                if (short.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(ushort) || type == typeof(ushort?))
            {
                ushort x;
                if (ushort.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                int x;
                if (int.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(uint) || type == typeof(uint?))
            {
                uint x;
                if (uint.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(long) || type == typeof(long?))
            {
                long x;
                if (long.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(ulong) || type == typeof(ulong?))
            {
                ulong x;
                if (ulong.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(float) || type == typeof(float?))
            {
                float x;
                if (float.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(double) || type == typeof(double?))
            {
                double x;
                if (double.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                decimal x;
                if (decimal.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(char) || type == typeof(char?))
            {
                char x;
                if (char.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(bool) || type == typeof(bool?))
            {
                bool x;
                if (bool.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                DateTime x;
                if (DateTime.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            else if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
            {
                DateTimeOffset x;
                if (DateTimeOffset.TryParse(value, out x))
                    return x;
                else
                    return null;
            }
            return null;
        }
    }

}
