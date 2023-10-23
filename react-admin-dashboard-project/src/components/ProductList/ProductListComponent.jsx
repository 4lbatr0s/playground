import React from 'react'
import "./ProductListComponent.css";
import { DeleteOutline } from '@mui/icons-material';
import { productRows } from '../../dummyData';
import { Link } from 'react-router-dom';
import { useState } from 'react';
import { DataGrid } from '@mui/x-data-grid';


const ProductListComponent = () => {
  const [data, setData] = useState(productRows);


  /**
   * @customFunctions
   */

   const handleDelete = (id) => {
      setData(data.filter((item) => item.id !== id));
    };

  const columns = [
      { field: 'id', headerName: 'ID', width: 70 },
      
      { field: 'product', headerName: 'Product', width: 200, renderCell:(params)=> {
          return ( /*INFO: DATAGRID: HOW TO CREATE DIFFERENT OUTPUTS! */
              <div className="productListItem">
                  <img className='productListImage' src={params.row.img} alt="" />
                  <span>{params.row.name}</span>
              </div>
          )
      } },
      {
          field: 'status',
          headerName: 'Status',
          type: 'String',
          width: 90,
      },
      {
          field: 'stock',
          headerName: 'Stock',
          width: 200,
      },
      {
          field: 'price',
          headerName: 'Price',
           width: 160,
      },
      {
          field: 'action',
          headerName: 'Action',
          width: 240,
          renderCell:(params)=>{
              return(
                  <>
                    <Link to ={`/product/${params.row.id}`}>                      
                      <button className="productListEditButton">Edit</button> {/*INFO: How to go edit page!*/}
                    </Link>
                      <DeleteOutline onClick= {()=> handleDelete(params.row.id)} className="userListDeleteButton"></DeleteOutline>                    
                  </>
              )
          }
      },

  ];

  //INFO: GRID DATA TABLE: HOW TO IMPLEMENT!
  return (
      <div className='productListComponent'>
          <DataGrid
              rows={data}
              columns={columns}
              disableSelectionOnClick //INFO: To prevent selection of an entire area when clicked
              pageSize={8}
              rowsPerPageOptions={[5]}
              checkboxSelection
          />
      </div>
  )
}

export default ProductListComponent