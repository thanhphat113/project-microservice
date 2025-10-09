import Login from "../Pages/Login.jsx";
import Register from "../Pages/Register.jsx";
import Tasks from "../Pages/Tasks.jsx";

export const privateRoutes = [{ path: "/", element: <Tasks /> }];

export const publicRoutes = [
    { path: "/login", element: <Login /> },
    { path: "/register", element: <Register /> },
];
