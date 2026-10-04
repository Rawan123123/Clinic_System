
import { useNavigate } from "react-router-dom";


function Welcome() {
    const navigate = useNavigate();
    return (
        <div>
            <h1>Welcome to Clinic System</h1>

            <button onClick={() => navigate("/login")}>Login</button>
            <button onClick={() => navigate("/register")}>Register</button>
        </div>
    )
}
export default Welcome;