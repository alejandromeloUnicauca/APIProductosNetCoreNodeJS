import { config } from 'dotenv';

config();

export const PORT = process.env.PORT || 3000;
export const DB_USER = process.env.DB_USER || '';  
export const DB_PASS = process.env.DB_PASSWORD || '';
export const DB_SERV = process.env.DB_SERVER || '';
export const DB_NAME = process.env.DB_DATABASE || '';