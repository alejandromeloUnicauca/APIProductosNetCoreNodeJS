import express from 'express'
import productRoutes from './routes/products.routes'
import cors from "cors";
import morgan from "morgan";

const app = express()

app.use(express.json());
app.use(cors());
app.use(morgan("dev"));
app.use(express.urlencoded({extended:false}));
app.use(productRoutes)

export default app