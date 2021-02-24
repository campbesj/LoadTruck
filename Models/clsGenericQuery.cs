using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;


namespace LoadTruck.Models
{
    public class clsGenericQuery 
    {


        #region Declarations
        private DataSet myDataSet = new DataSet();
        public DataSet MyDataSet
        {
            get { return myDataSet; }
            set { myDataSet = value; }
        }
        private DataTable myDataTable = new DataTable();

        public DataTable MyDataTable
        {
            get { return myDataTable; }
            set { myDataTable = value; }
        }

        private List<DataTable> myDataTables = new List<DataTable>();

        public List<DataTable> MyDataTables
        {
            get { return myDataTables; }
            set { myDataTables = value; }
        }

        private System.Data.SqlClient.SqlDataAdapter myDataAdapter;

        public System.Data.SqlClient.SqlDataAdapter MyDataAdapter
        {
            get { return myDataAdapter; }
            set { myDataAdapter = value; }
        }

        List<System.Data.SqlClient.SqlDataAdapter> myDataAdapters = new List<SqlDataAdapter>();

        public List<System.Data.SqlClient.SqlDataAdapter> MyDataAdapters
        {
            get { return myDataAdapters; }
            set { myDataAdapters = value; }
        }

        private DataRow myRow;

        public DataRow MyRow
        {
            get { return myRow; }
            set { myRow = value; }

        }
        private List<DataRow> myRows = new List<DataRow>();

        public List<DataRow> MyRows
        {
            get { return myRows; }
            set { myRows = value; }

        }



        public Boolean IsNew = false;
        public List<bool> IsNews = new List<bool>();
        public System.Data.SqlClient.SqlCommandBuilder MyCmd;
        public List<System.Data.SqlClient.SqlCommandBuilder> MyCmds = new List<SqlCommandBuilder>();
        private System.Data.SqlClient.SqlConnection myCon;

        public System.Data.SqlClient.SqlConnection MyCon
        {
            get { return myCon; }
            set { myCon = value; }
        }
        public int id = 0; 
        #endregion

        #region Constructors

        /// <summary>
        /// Constructor for using parameters
        /// </summary>
        /// <param name="Query">The query string</param>
        /// <param name="Params">A hashtable that contains the name and values of the parameters used by the query</param>
        /// <param name="ConnectionString">ConnectionString</param>
        public clsGenericQuery(string Query, Hashtable Params, string ConnectionString)
        {
            MyDataSet.Tables.Clear();

            this.MyCon = new System.Data.SqlClient.SqlConnection();
            this.MyCon.ConnectionString = ConnectionString;

            this.MyCon.Open();


            this.MyDataAdapter = new System.Data.SqlClient.SqlDataAdapter(Query, this.MyCon);
            this.MyCmd = new System.Data.SqlClient.SqlCommandBuilder(this.MyDataAdapter);


            foreach (object o in Params.Keys)
            {
                string keyname = o.ToString();

                if (!(keyname.StartsWith("@")))
                {
                    keyname = "@" + keyname;
                }
                this.MyDataAdapter.SelectCommand.Parameters.AddWithValue(keyname, Params[keyname]);
                //this.MyDataAdapter.UpdateCommand.Parameters.AddWithValue(keyname, QueryAndParams[keyname]);

            }


            this.MyDataAdapter.Fill(this.MyDataSet, "analysis");
            this.MyDataTable = this.MyDataSet.Tables[0];


            this.MyCon.Close();

            if (this.MyDataTable.Rows.Count == 0)
            {
                this.IsNew = true;
                this.MyRow = this.MyDataTable.NewRow();
                this.MyDataTable.Rows.Add(this.MyRow);
            }
            else
            {
                this.IsNew = false;
                this.MyRow = this.MyDataTable.Rows[0];
            }
            if (this.MyDataTable.Columns.Contains("touched"))
            {
                //this.MyRow["touched"] = clsGlobals.touched + "|" + DateTime.Now.ToString();
            }
        }//constructor

        public clsGenericQuery(string Query, string ConnectionString)
        {
            if (Query.Contains(";"))
            {
                return;
            }
            MyDataSet.Tables.Clear();

            this.MyCon = new System.Data.SqlClient.SqlConnection();
            this.MyCon.ConnectionString = ConnectionString;

            this.MyCon.Open();
            this.MyDataTable.Clear();
            this.MyDataTable.Rows.Clear();
            this.MyDataTable.Columns.Clear();
            this.MyDataAdapter = new System.Data.SqlClient.SqlDataAdapter(Query, this.MyCon);
            this.MyDataAdapter.SelectCommand.CommandTimeout = 3000;




            this.MyDataAdapter.Fill(this.MyDataSet, "analysis");
            this.MyDataTable = this.MyDataSet.Tables[0];

            this.MyCmd = new System.Data.SqlClient.SqlCommandBuilder(this.MyDataAdapter);

            this.MyCon.Close();

            if (this.MyDataTable.Rows.Count == 0)
            {
                this.IsNew = true;
                this.MyRow = this.MyDataTable.NewRow();
                this.MyDataTable.Rows.Add(this.MyRow);
            }
            else
            {
                this.IsNew = false;
                this.MyRow = this.MyDataTable.Rows[0];
            }
            if (this.MyDataTable.Columns.Contains("touched"))
            {
                //this.MyRow["touched"] = clsGlobals.touched + "|" + DateTime.Now.ToString();
            }
        }//constructor

        public clsGenericQuery(string[] Queries, string ConnectionString)
        {
            MyDataSet.Tables.Clear();

            this.MyCon = new System.Data.SqlClient.SqlConnection();
            this.MyCon.ConnectionString = ConnectionString;

            this.MyCon.Open();
            int counter = 0;
            foreach (string Query in Queries)
            {
                if (Query.Contains(";"))
                {
                    return;
                }
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(Query, this.MyCon);
                da.Fill(this.MyDataSet, "analysis" + counter);
                DataTable dt = this.MyDataSet.Tables["analysis" + counter];

                System.Data.SqlClient.SqlCommandBuilder cmd = new System.Data.SqlClient.SqlCommandBuilder(da);

                this.myDataAdapters.Add(da);
                this.MyCmds.Add(cmd);

                if (dt.Rows.Count == 0)
                {
                    this.IsNews.Add(true);
                    DataRow dr = dt.NewRow();
                    this.MyRows.Add(dr);
                    dt.Rows.Add(dr);
                }
                else
                {
                    this.IsNews.Add(false);
                    this.MyRows.Add(dt.Rows[0]);
                }
                this.MyDataTables.Add(dt);
                counter++;
            }

            this.MyCon.Close();

        }//constructor



        public clsGenericQuery(string Query, string ConnectionString, bool WillInsert)
        {
            if (Query.Contains(";"))
            {
                return;
            }
            MyDataSet.Tables.Clear();

            this.MyCon = new System.Data.SqlClient.SqlConnection();
            this.MyCon.ConnectionString = ConnectionString;

            this.MyCon.Open();

            this.MyDataAdapter = new System.Data.SqlClient.SqlDataAdapter(Query, this.MyCon);
            this.MyCmd = new System.Data.SqlClient.SqlCommandBuilder(this.MyDataAdapter);
            this.MyDataAdapter.RowUpdated += new System.Data.SqlClient.SqlRowUpdatedEventHandler(My_OnRowUpdate);


            this.MyDataAdapter.DeleteCommand = (System.Data.SqlClient.SqlCommand)this.MyCmd.GetDeleteCommand().Clone();
            this.MyDataAdapter.UpdateCommand = (System.Data.SqlClient.SqlCommand)this.MyCmd.GetUpdateCommand().Clone();

            // now we modify the INSERT command, first we clone it and then modify
            System.Data.SqlClient.SqlCommand cmd = (System.Data.SqlClient.SqlCommand)this.MyCmd.GetInsertCommand().Clone();

            // adds the call to SCOPE_IDENTITY                                      
            cmd.CommandText += " SET @NEWID = SCOPE_IDENTITY()";
            // the SET command writes to an output parameter "@ID"
            System.Data.SqlClient.SqlParameter parm = new System.Data.SqlClient.SqlParameter();
            parm.Direction = ParameterDirection.Output;
            parm.Size = 4;
            parm.SqlDbType = System.Data.SqlDbType.Int;
            parm.ParameterName = "@NEWID";
            parm.DbType = DbType.Int32;

            // adds parameter to command
            cmd.Parameters.Add(parm);

            // adds our customized insert command to DataAdapter
            this.MyDataAdapter.InsertCommand = cmd;

            // CommandBuilder needs to be disposed otherwise 
            // it still tries to generate its default INSERT command 
            this.MyCmd.Dispose();

            this.MyDataAdapter.Fill(this.MyDataSet, "analysis");
            this.MyDataTable = this.MyDataSet.Tables[0];



            this.MyCon.Close();

            if (this.MyDataTable.Rows.Count == 0)
            {
                this.IsNew = true;
                this.MyRow = this.MyDataTable.NewRow();
                this.MyDataTable.Rows.Add(this.MyRow);
            }
            else
            {
                this.IsNew = false;
                this.MyRow = this.MyDataTable.Rows[0];
            }
            if (this.MyDataTable.Columns.Contains("touched"))
            {
                // this.MyRow["touched"] = clsGlobals.touched + "|" + DateTime.Now.ToString();
            }
        }//constructor
        
        #endregion


        #region Updaters
        public void update()
        {
            this.MyCon.Open();
            // this.MyDataAdapter.UpdateCommand = new SqlCommand();
            if (this.MyDataAdapter.SelectCommand.CommandText.Contains("@"))
            {
                this.MyDataAdapter.SelectCommand.CommandText = this.MyDataAdapter.SelectCommand.CommandText.Replace("@", "");
                this.MyDataAdapter.SelectCommand.Parameters.Clear();
            }
            
            this.MyDataAdapter.Update(this.MyDataTable);

            this.MyCon.Close();
        }//update       

        public void transactionUpdate()
        {
            this.MyCon.Open();

            if (this.MyDataAdapters.Count > 0)
            {
                int elements = this.MyDataAdapters.Count;
                List<SqlCommand> siteInsert = new List<SqlCommand>();
                List<SqlCommand> siteUpdate = new List<SqlCommand>();
                for (int x = 0; x < elements; x++)
                {
                    // this.MyDataAdapter.UpdateCommand = new SqlCommand();
                    if (this.MyDataAdapters[x].SelectCommand.CommandText.Contains("@"))
                    {
                        this.MyDataAdapters[x].SelectCommand.CommandText = this.MyDataAdapter.SelectCommand.CommandText.Replace("@", "");
                        this.MyDataAdapters[x].SelectCommand.Parameters.Clear();
                    }


                    siteInsert.Add(this.MyCmds[x].GetInsertCommand());
                    siteUpdate.Add(this.MyCmds[x].GetUpdateCommand());

                }
                SqlTransaction tran = this.MyCon.BeginTransaction();
                for (int x = 0; x < elements; x++)
                {
                    siteInsert[x].Transaction = tran;
                    siteUpdate[x].Transaction = tran;
                }

                try
                {
                    for (int x = 0; x < elements; x++)
                    {
                        this.MyDataAdapters[x].Update(this.MyDataTables[x]);
                    }
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    //Roll back the transaction.
                    tran.Rollback();

                    //Additional error handling if needed.
                }
                finally
                {
                    // Close the connection.
                    this.MyCon.Close();
                }

            }//************************************************************************
            else
            {
                // this.MyDataAdapter.UpdateCommand = new SqlCommand();
                if (this.MyDataAdapter.SelectCommand.CommandText.Contains("@"))
                {
                    this.MyDataAdapter.SelectCommand.CommandText = this.MyDataAdapter.SelectCommand.CommandText.Replace("@", "");
                    this.MyDataAdapter.SelectCommand.Parameters.Clear();
                }


                SqlCommand siteInsert = this.MyCmd.GetInsertCommand();
                SqlCommand siteUpdate = this.MyCmd.GetUpdateCommand();
                SqlTransaction tran = this.MyCon.BeginTransaction();


                siteInsert.Transaction = tran;
                siteUpdate.Transaction = tran;
                try
                {
                    this.MyDataAdapter.Update(this.MyDataTable);
                    tran.Commit();
                }
                catch (SqlException ex)
                {
                    //Roll back the transaction.
                    tran.Rollback();

                    //Additional error handling if needed.
                }
                finally
                {
                    // Close the connection.
                    this.MyCon.Close();
                }
            }

        }//update



        public void insert()
        {
            this.MyCon.Open();
            int counter = 0;
            foreach (DataColumn c in MyDataTable.Columns)
            {
                if (!(c.ColumnName.ToString() == "id"))
                {
                    counter++;
                    // this.MyDataAdapter.InsertCommand.Parameters.AddWithValue("@p" + counter.ToString(), this.MyRow[c.ColumnName]);
                    this.MyDataAdapter.InsertCommand.Parameters["@p" + counter.ToString()].Value = this.MyRow[c.ColumnName];
                }
            }

            this.MyDataAdapter.Update(this.MyDataTable);
            this.MyCon.Close();
        }//upd
        #endregion

        public int Col(string s)
        {
            return this.MyDataTable.Columns[s].Ordinal;
        }//Col


        private void My_OnRowUpdate(object sender, System.Data.SqlClient.SqlRowUpdatedEventArgs e)
        {
            if (e.StatementType == StatementType.Insert)
            {
                // reads the identity value from the output parameter @ID
                object ai = e.Command.Parameters["@NEWID"].Value;
                this.id = (int)ai;
                // updates the identity column (autoincrement)                   

                DataColumn C = this.MyDataTable.Columns[0];
                if (C.ColumnName == "id")
                {
                    C.ReadOnly = false;
                    e.Row[C] = ai;
                    C.ReadOnly = true;

                }


                e.Row.AcceptChanges();
            }
        }


        public static Boolean RunCommand(string query, string ConnectionString)
        {
            SqlTransaction transaction = null;
            SqlConnection conn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            try
            {

                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;

                cmd.ExecuteNonQuery();

                conn.Close();


                //MySqlCommand psmt = new MySqlCommand();
                return true;

            }
            catch (Exception ex)
            {
               // Utilities.Logit(ex.Message);
                return false;
            }
            finally
            {

            }
        }

        public static Boolean RunCommand(string query,Hashtable Params, string ConnectionString)
        {
            SqlTransaction transaction = null;
            SqlConnection conn = new SqlConnection(ConnectionString);

            SqlCommand cmd = new SqlCommand();
            try
            {

                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                foreach (object o in Params.Keys)
                {
                    string keyname = o.ToString();

                    if (!(keyname.StartsWith("@")))
                    {
                        keyname = "@" + keyname;
                    }

                    cmd.Parameters.AddWithValue(keyname, Params[keyname]);
                    //this.MyDataAdapter.SelectCommand.Parameters.AddWithValue(keyname, Params[keyname]);
                    //this.MyDataAdapter.UpdateCommand.Parameters.AddWithValue(keyname, QueryAndParams[keyname]);

                }


                cmd.ExecuteNonQuery();

                conn.Close();


                //MySqlCommand psmt = new MySqlCommand();
                return true;

            }
            catch (Exception ex)
            {
                // Utilities.Logit(ex.Message);
                return false;
            }
            finally
            {

            }
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int counter = 1;
            foreach (DataRow r in this.myDataTable.Rows)
            {
                sb.Append(counter + " -------------------------------------" + Environment.NewLine);
                counter++;
                foreach (DataColumn c in this.MyDataTable.Columns)
                {
                    sb.Append(c.ColumnName + " = " + noNull(r[c.ColumnName]) + "" + Environment.NewLine);
                }


            }
            return sb.ToString();
        }


        public static int GetUniqueID(string ConnectionString)
        {
            clsGenericQuery gq = new clsGenericQuery("select * from GetUnique where id=0", ConnectionString, true);
            gq.MyRow["Nothing"] = "X";
            gq.insert();
            return gq.id;
        }


        private Dictionary<Type, DbType> TypeDict()
        {
            Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>();
            typeMap[typeof(byte)] = DbType.Byte;
            typeMap[typeof(sbyte)] = DbType.SByte;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(ushort)] = DbType.UInt16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(uint)] = DbType.UInt32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(ulong)] = DbType.UInt64;
            typeMap[typeof(float)] = DbType.Single;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(bool)] = DbType.Boolean;
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char)] = DbType.StringFixedLength;
            typeMap[typeof(Guid)] = DbType.Guid;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(byte[])] = DbType.Binary;
            typeMap[typeof(byte?)] = DbType.Byte;
            typeMap[typeof(sbyte?)] = DbType.SByte;
            typeMap[typeof(short?)] = DbType.Int16;
            typeMap[typeof(ushort?)] = DbType.UInt16;
            typeMap[typeof(int?)] = DbType.Int32;
            typeMap[typeof(uint?)] = DbType.UInt32;
            typeMap[typeof(long?)] = DbType.Int64;
            typeMap[typeof(ulong?)] = DbType.UInt64;
            typeMap[typeof(float?)] = DbType.Single;
            typeMap[typeof(double?)] = DbType.Double;
            typeMap[typeof(decimal?)] = DbType.Decimal;
            typeMap[typeof(bool?)] = DbType.Boolean;
            typeMap[typeof(char?)] = DbType.StringFixedLength;
            typeMap[typeof(Guid?)] = DbType.Guid;
            typeMap[typeof(DateTime?)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
            //typeMap[typeof(Binary)] = DbType.Binary;
            return typeMap;
        }




        string noNull(object o)
        {
            if (o is DBNull)
            {
                return "NULL";
            }
            return o.ToString();
        }



    }//class

}
