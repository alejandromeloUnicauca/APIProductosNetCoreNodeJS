import sql from "mssql";
import {DB_USER,DB_PASS,DB_SERV,DB_NAME, PORT} from '../config';

const dbSettings = {
    user: DB_USER,
    password: DB_PASS,
    server: DB_SERV,
    database: DB_NAME,
    options:{
        encrypt:true,
        trustServerCertificate:true,
    }
}

export const getConnection = async () => {
    try {
      const pool = await sql.connect(dbSettings);
      return pool;
    } catch (error) {
      console.error(error);
    }
  };
  
  export { sql };
