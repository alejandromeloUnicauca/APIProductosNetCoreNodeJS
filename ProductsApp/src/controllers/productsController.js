import { getConnection, sql } from "../database/connection";
import { Success, Error, NotFound, BadRequest } from "../Utils/Constantes";

export const deleteProductById = async (req, res) => {
    try {
        const Response = require('../DTOs/Response'); 
        const pool = await getConnection();
        const result = await pool
        .request()
        .input("Id",sql.Int, req.params.Id)
        .execute("DeleteProduct");
        if (result.rowsAffected[0] === 0) return res.status(200).json(new Response(404,NotFound,null));
  
        return res.status(200).json(new Response(200,Success,null));
    } catch (error) {
        console.log(error)
        res.status(500);
        res.send(error.message);
    }
  };

export const updateProductById = async (req, res) => {
    const Response = require('../DTOs/Response'); 
    const Id = req.params.Id
    const {name, description, price, stock } = req.body;
    if (!name || !description || !price || !stock ) 
        return res.status(200).json(new Response(400,BadRequest,null));

    try {
        const pool = await getConnection();

        const result = await pool
        .request()
        .input("Id",sql.Int,Id)
        .input("Name",sql.VarChar,name)
        .input("Description",sql.VarChar,description)
        .input("Price",sql.Decimal,price)
        .input("Stock",sql.Int,stock)
        .execute("UpdateProduct");
        if (result.rowsAffected[0] === 0) return res.sendStatus(404);
  
        return res.status(200).json(new Response(200,Success,{id: Id, name:name, description:description, price:price, stock:stock }));
    } catch (error) {

        return res.send(new Response(500,Error,null));
    }
  };