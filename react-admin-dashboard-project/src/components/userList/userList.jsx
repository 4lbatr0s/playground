import React, { useState } from 'react';
import "./userList.css";
import { DataGrid } from '@mui/x-data-grid';
import {DeleteOutline} from '@mui/icons-material';
import { userRows } from '../../dummyData';
import { Link } from 'react-router-dom';

const UserListComp = () => {

    const [data, setData] = useState(userRows);


    /**
     * @customFunctions
     */

     const handleDelete = (id) => {
        setData(data.filter((item) => item.id !== id));
      };

    const columns = [
        { field: 'id', headerName: 'ID', width: 70 },
        
        { field: 'user', headerName: 'User', width: 200, renderCell:(params)=> {
            return ( /*INFO: DATAGRID: HOW TO CREATE DIFFERENT OUTPUTS! */
                <div className="userNameUser">
                    <img className='userListImage' src={params.row.avatar} alt="" />
                    <span>{params.row.username}</span>
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
            field: 'email',
            headerName: 'Email',
            width: 200,
        },
        {
            field: 'transaction',
            headerName: 'Transaction',
             width: 160,
        },
        {
            field: 'action',
            headerName: 'Action',
            width: 240,
            renderCell:(params)=>{
                return(
                    <>
                      <Link to ={`/user/${params.row.id}`}>                      
                        <button className="userListEditButton">Edit</button> {/*INFO: How to go edit page!*/}
                      </Link>
                        <DeleteOutline onClick= {()=> handleDelete(params.row.id)} className="userListDeleteButton"></DeleteOutline>                    
                    </>
                )
            }
        },

    ];

    //INFO: GRID DATA TABLE: HOW TO IMPLEMENT!
    return (
        <div className='userList'>
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

export default UserListComp