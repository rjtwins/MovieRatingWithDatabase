using System.Diagnostics;

namespace MovieRatingWithDatabase.ResultDataSetTableAdapters
{
    public partial class resultTableAdapter
    {
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, false)]
        public virtual int UpdateQuery(object[] arguments)
        {
            global::System.Data.SqlClient.SqlCommand command = this.CommandCollection[1];
            for (int i = 0; i < arguments.Length; i++)
            {
                System.Data.SqlClient.SqlParameter parameter = this.Adapter.InsertCommand.Parameters[i + 1];
                object argument = arguments[i];
                Debug.WriteLine("Argument content" + argument.ToString());
                Debug.WriteLine("Argument: " + argument.GetType());
                Debug.WriteLine("Parameter: " + parameter.DbType);

                if (argument == null)
                {
                    if (i == 1)
                    {
                        throw new global::System.ArgumentNullException("id");
                    }
                    parameter.Value = global::System.DBNull.Value;
                    continue;
                }
                parameter.Value = argument;
            }
            global::System.Data.ConnectionState previousConnectionState = command.Connection.State;
            if (((command.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                command.Connection.Open();
            }
            int returnValue;
            try
            {
                returnValue = command.ExecuteNonQuery();
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    command.Connection.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Very unsafe with very little safty checks, use at own risk!
        /// When given a list of objects incasulating columns values in a row in the correct order and type, will insert the row.
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns>int returnValue</returns>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "16.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(object[] arguments)
        {
            for (int i = 0; i < arguments.Length; i++)
            {
                System.Data.SqlClient.SqlParameter parameter = this.Adapter.InsertCommand.Parameters[i];
                object argument = arguments[i];
                if (argument == null)
                {
                    if (i == 0)
                    {
                        throw new global::System.ArgumentNullException("id");
                    }
                    parameter.Value = global::System.DBNull.Value;
                    continue;
                }
                Debug.WriteLine("Argument: " + argument.GetType());
                Debug.WriteLine("Parameter: " + parameter.DbType);
                parameter.Value = argument;
            }

            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try
            {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
    }
}

namespace MovieRatingWithDatabase
{
    partial class ResultDataSet
    {
    }
}