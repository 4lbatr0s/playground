import React from 'react'
import ReactDOM from 'react-dom/client'
import { RouterProvider } from 'react-router';
import App from './App'
import './index.css';
import Routes from "./Routes"

ReactDOM.createRoot(document.getElementById('root')).render(
  <RouterProvider router = {Routes}>
  </RouterProvider>
)
