using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Pinomapper.core;

namespace Pinomapper
{
    public class Pinomapper
    {
        public TR Map<TR>(object source)
        {
            try
            {
                var isCollection = source is IEnumerable;
                if (isCollection)
                {
                    var sourceList = (IEnumerable) source;
                    var result = Activator.CreateInstance<TR>();
                    var sourceType = ((IEnumerable<object>)sourceList).FirstOrDefault()?.GetType();
                    if (sourceType == null) return result;
                    var resultType = typeof(TR).GenericTypeArguments.FirstOrDefault()?.UnderlyingSystemType;
                    if (resultType == null) return result;
                    var properties = resultType.GetProperties();
                    foreach (var item in (IEnumerable<object>)source)
                    {
                        var copiedObject = FullCopy.Go(item);
                        var newItem = Activator.CreateInstance(resultType);
                        foreach (var property in properties)
                        {
                            var propName = property.Name;
                            var sourceProp = sourceType.GetProperty(propName);
                            if (sourceProp != null)
                            {
                               property.SetValue(newItem, sourceProp.GetValue(copiedObject));
                            }
                        }
                        (result as IList)?.Add(newItem);
                    }
                    return result;
                }
                else
                {
                    var result = Activator.CreateInstance<TR>();
                    var sourceType = source.GetType();
                    var resultType = typeof(TR);
                    var properties = resultType.GetProperties();
                    var copiedObject = FullCopy.Go(source);
                    foreach (var property in properties)
                    {
                        var propName = property.Name;
                        var sourceProp = sourceType.GetProperty(propName);
                        if (sourceProp != null)
                        {
                            property.SetValue(result, sourceProp.GetValue(copiedObject));
                        }
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Pinomapper Map Error: {e.Message}\n{e.StackTrace}");
            }
        }
    }
}
