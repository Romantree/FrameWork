using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TS.FW.Diagnostics
{
    /// <summary>
    /// 코드 디컴파일 처리 함수 
    /// ( 박현식 책임 해당 소스 개발 )
    /// </summary>
    public static class CodeDomHandler
    {
        public static Response MakeCSharpDllFile(this string filePath, bool isDebug, string[] codFile, params string[] referencedAssemblies)
        {
            try
            {
                var dir = Path.GetDirectoryName(filePath);

                if (Directory.Exists(dir) == false) Directory.CreateDirectory(dir);
                if (File.Exists(filePath)) File.Delete(filePath);

                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    var cParams = new CompilerParameters()
                    {
                        GenerateExecutable = false,
                        IncludeDebugInformation = isDebug,
                        OutputAssembly = filePath,
                    };

                    cParams.ReferencedAssemblies.AddRange(referencedAssemblies);

                    var res = provider.CompileAssemblyFromFile(cParams, codFile);

                    if (res.Errors.Count > 0)
                    {
                        var message = string.Join(Environment.NewLine, res.Errors.OfType<object>().Select(t => t.ToString()));

                        return new Response(false, message);
                    }
                }

                return new Response();
            }
            catch (Exception ex)
            {
                Logger.Write(typeof(CodeDomHandler), ex);
                return ex;
            }
        }
    }
}
