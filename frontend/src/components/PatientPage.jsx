import { useNavigate } from "react-router-dom";
import { usePatients } from "../hooks/usePatients";
function PatientPage() {
    const { patients, loading, error } = usePatients();
    const navigate = useNavigate();

    function getGender(gender) {
        return gender === 0 ? "Male" : "Female";
    }
    function handleLogOut() {
        localStorage.removeItem("MyToken")
        navigate("/login");
    }

    if (loading) return <p>Loading...</p>
    if (error) return <p>{error}</p>


    return(
        <div>
            <h2>My Patients</h2>
            <ul>
                {patients.map(p => (
                    <li key={p.patientId}>
                        {`Name: ${p.name}, Age: ${p.age}, Gender: ${getGender(p.gender)}`}

                    </li>
                ))}
            </ul>

            <button type="button" onClick={() => navigate("/")}>Home</button>
            <button type="button" onClick={() => {handleLogOut()}}>LogOut</button>

        </div>
    )
    

}
export default PatientPage;