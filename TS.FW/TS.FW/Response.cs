using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TS.FW.Utils;

namespace TS.FW
{
    /// <summary>
    /// 단순 응답 클래스
    /// </summary>
    [DataContract]
    public class Response
    {
        /// <summary>
        /// 성공 여부
        /// </summary>
        [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// 실패 사유
        /// </summary>
        [DataMember]
        public string Comment { get; set; }

        /// <summary>
        /// 무인수 생성자
        /// </summary>
        public Response()
        {
            this.Success = true;
            this.Comment = string.Empty;
        }

        /// <summary>
        /// 오류 생성자
        /// </summary>
        /// <param name="ex">오류 정보</param>
        public Response(Exception ex)
        {
            this.Success = false;
            this.Comment = ex == null ? string.Empty : ex.MakeExceptionMessage();
        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="success">성공 여부</param>
        /// <param name="comment">실패 사유</param>
        public Response(bool success, string comment)
        {
            Success = success;
            Comment = comment;
        }

        public Response(bool success, string format, params object[] parm)
        {
            this.Success = success;
            this.Comment = string.Format(format, parm);
        }

        public override string ToString()
        {
            return this.Comment;
        }

        public static implicit operator bool(Response res)
        {
            return res.Success;
        }

        public static implicit operator string(Response res)
        {
            return res.Comment;
        }

        public static implicit operator Response(Exception ex)
        {
            return new Response(ex);
        }
    }
    /// <summary>
    /// 결과 값 응답 클래스
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class Response<T> : Response
    {
        /// <summary>
        /// 결과 값
        /// </summary>
        [DataMember]
        public T Result { get; set; }

        public Response() : base()
        {

        }

        public Response(Exception ex) : base(ex)
        {

        }

        public Response(bool success, string comment) : base(success, comment)
        {

        }

        public Response(bool success, string format, params object[] parm) : base(success, format, parm)
        {

        }

        public Response(T result) : base()
        {
            this.Result = result;
        }

        public static implicit operator bool(Response<T> res)
        {
            return res.Success;
        }

        public static implicit operator string(Response<T> res)
        {
            return res.Comment;
        }

        public static implicit operator Response<T>(Exception ex)
        {
            return new Response<T>(ex);
        }
    }

    [DataContract]
    public class Response<TKey, TValue> : Response
    {
        [DataMember]
        public TKey Key { get; set; }

        [DataMember]
        public TValue Value { get; set; }

        public Response() : base()
        {

        }

        public Response(Exception ex) : base(ex)
        {
        }

        public Response(bool success, string comment) : base(success, comment)
        {

        }

        public Response(bool success, string format, params object[] parm) : base(success, format, parm)
        {

        }

        public Response(TKey key, TValue value) : base()
        {
            this.Key = key;
            this.Value = value;
        }

        public static implicit operator bool(Response<TKey, TValue> res)
        {
            return res.Success;
        }

        public static implicit operator string(Response<TKey, TValue> res)
        {
            return res.Comment;
        }

        public static implicit operator Response<TKey, TValue>(Exception ex)
        {
            return new Response<TKey, TValue>(ex);
        }
    }

    [DataContract]
    public class ResponseList<T> : Response
    {
        [DataMember]
        public List<T> Result { get; set; }

        public ResponseList() : base()
        {

        }

        public ResponseList(Exception ex) : base(ex)
        {

        }

        public ResponseList(bool success, string comment) : base(success, comment)
        {

        }

        public ResponseList(bool success, string format, params object[] parm) : base(success, format, parm)
        {

        }

        public ResponseList(IEnumerable<T> list) : base()
        {
            this.Result = new List<T>(list);
        }

        public static implicit operator bool(ResponseList<T> res)
        {
            return res.Success;
        }

        public static implicit operator string(ResponseList<T> res)
        {
            return res.Comment;
        }

        public static implicit operator ResponseList<T>(Exception ex)
        {
            return new ResponseList<T>(ex);
        }
    }

    [DataContract]
    public class ResponseList<TKey, TValue> : Response
    {
        [DataMember]
        public Dictionary<TKey, TValue> Result { get; set; }

        public ResponseList() : base()
        {

        }

        public ResponseList(Exception ex) : base(ex)
        {

        }

        public ResponseList(bool success, string comment) : base(success, comment)
        {

        }

        public ResponseList(bool success, string format, params object[] parm) : base(success, format, parm)
        {

        }

        public ResponseList(IDictionary<TKey, TValue> list)
        {
            this.Result = new Dictionary<TKey, TValue>(list);
        }

        public static implicit operator bool(ResponseList<TKey, TValue> res)
        {
            return res.Success;
        }

        public static implicit operator string(ResponseList<TKey, TValue> res)
        {
            return res.Comment;
        }

        public static implicit operator ResponseList<TKey, TValue>(Exception ex)
        {
            return new ResponseList<TKey, TValue>(ex);
        }
    }
}
