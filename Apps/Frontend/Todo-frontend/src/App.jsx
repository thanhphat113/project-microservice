import { Routes, Route, Navigate } from "react-router-dom";
import { privateRoutes, publicRoutes } from "./Routes/routes";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { authActions } from "./Redux/Actions/AuthActions";
import Loading from "./Pages/Loading";
import PublicRoute from "./Components/Routes/PublicRoute";
import PrivateRoute from "./Components/Routes/PrivateRoute";
import GlobalNotification from "./Components/Notifications/GlobalNotification";
import { AnimatePresence } from "framer-motion";

function App() {
    const dispatch = useDispatch();
    const [loading, setLoading] = useState(true);

    const { message, isSuccess, isError, isLoading } = useSelector(
        (state) => state.status
    );

    const { information } = useSelector((state) => state.auth);

    useEffect(() => {
        const getUser = async () => {
            try {
                await dispatch(authActions.getCurrentUser());
            } catch (error) {
                console.log("Lỗi", error);
            } finally {
                setLoading(false);
            }
        };

        !information && getUser();
    }, []);

    if (loading) return <Loading />;

    return (
        <div className="w-full h-screen ">
            <AnimatePresence>
                {message && (
                    <GlobalNotification
                        message={message}
                        isLoading={isLoading}
                        isError={isError}
                        isSuccess={isSuccess}
                    />
                )}
            </AnimatePresence>
            <>
                <Routes>
                    {publicRoutes.map((route, index) => (
                        <Route
                            key={index}
                            path={route.path}
                            element={<PublicRoute>{route.element}</PublicRoute>}
                        />
                    ))}

                    {privateRoutes.map((route, index) => (
                        <Route
                            key={index}
                            path={route.path}
                            element={
                                <PrivateRoute>{route.element}</PrivateRoute>
                            }
                        />
                    ))}
                </Routes>
            </>
        </div>
    );
}

export default App;
