import React from 'react'
import { Link } from 'react-router-dom';
import './ProductComponent.css';
import Chart from "../chart/Chart";
import { productData } from '../../dummyData';
import { Publish } from '@mui/icons-material';

const ProductComponent = () => {
  return (
    <div className='productComponent'>
      <div className="productTitleContainer">
        <h1 className="productTitle">Product</h1>
        <Link to ="/newProduct">
          <button className="productAddButton">Create</button>
        </Link>
      </div>
      <div className="productTop">
        <div className="productTopLeft">
          <Chart data = {productData} dataKey="Sales" title="Sales Performance"/>
        </div>
        <div className="productTopRight">
          <div className="productInfoTop">
            <img src="https://images.pexels.com/photos/7156886/pexels-photo-7156886.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500" alt="" className="productInfoImg" />
            <span className="productName">Apple Airpods</span>
          </div>
          <div className="productInfoBottom">
            <div className="productInfoItem">
              <span className="productInfoKey">Id:</span>
              <span className="productInfoValue">123</span>
            </div>
            <div className="productInfoItem">
              <span className="productInfoKey">Sales:</span>
              <span className="productInfoValue">112512</span>
            </div>
            <div className="productInfoItem">
              <span className="productInfoKey">Actie:</span>
              <span className="productInfoValue">Yes</span>
            </div>
            <div className="productInfoItem">
              <span className="productInfoKey">In Stock:</span>
              <span className="productInfoValue">Yes</span>
            </div>
          </div>
        </div>
      </div>
      <div className="productBottom">
        <form action="" className="productForm">
          <div className="productFormLeft">
            <label htmlFor="productName" id='productName'>Product Name</label>
            <input type="text" placeholder='Apple Airpod' />
            <label htmlFor="inStock">In Stock</label>
            <select name="inStock" id="inStock">
              <option value="yes">Yes</option>
              <option value="no">No</option>
            </select>
            <label htmlFor="active">Active</label>
            <select name="isActive" id="isActive">
              <option value="yes">Yes</option>
              <option value="no">No</option>
            </select>
          </div>
          <div className="productFormRight">
            <div className="productUpload">
              <img src="https://images.pexels.com/photos/7156886/pexels-photo-7156886.jpeg?auto=compress&cs=tinysrgb&dpr=2&w=500" alt="" className="productUploadImg"/>
              <label htmlFor="file">
                <Publish/>
              </label>
              <input type="file"  id="file" style={{display:"none"}}/>
            </div>
            <button className="productButton">Update</button>
          </div>
        </form>
      </div>
    </div>
  )
}
 
export default ProductComponent