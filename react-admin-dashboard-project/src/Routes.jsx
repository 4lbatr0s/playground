import { createBrowserRouter, Route, createRoutesFromElements, useNavigate, Navigate } from 'react-router-dom';
import App from './App';
import NewUser from './pages/newUser/NewUser';
import Product from './pages/Product/Product';
import ProductList from './pages/productList/ProductList';
import User from './pages/user/User';
import UserList from './pages/userlist/UserList';


const projectRouter = createBrowserRouter(
    createRoutesFromElements( //INFO: HOW TO CREATE NESTED ROUTES!
        <>
            <Route path="/" element={<App />}/>
            <Route path ='/users' element = {<UserList/>}/>
            <Route path ='/user/:userId' element = {<User/>}/>
            <Route path ='/newUser' element = {<NewUser/>}/>
            <Route path ='/products' element = {<ProductList/>}/>
            <Route path ='/product/:productId' element = {<Product/>}/>
        </>
    )
);

export default projectRouter;



