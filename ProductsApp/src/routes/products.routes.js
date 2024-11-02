import { Router } from 'express';
import { deleteProductById, updateProductById } from '../controllers/productsController';

const router = Router();

router.delete("/api/Product/:Id", deleteProductById);

router.put("/api/Product/:Id", updateProductById);

export default router;