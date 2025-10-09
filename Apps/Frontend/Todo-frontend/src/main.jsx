import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import "./main.css";
import { Provider } from "react-redux";
import store from "./Redux/store.jsx";
import { BrowserRouter as Router } from "react-router-dom";

createRoot(document.getElementById("root")).render(
    <StrictMode>
        <Router>
            <Provider store={store}>
                <App />
            </Provider>
        </Router>
    </StrictMode>
);
