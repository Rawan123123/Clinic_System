import { useState , useEffect } from 'react'
import { useNavigate } from "react-router-dom";
import { API_BASE_URL } from '../config';

function PatientPage() {

    const [patients, setPatients] = useState([])
    const [loading, setLoading] = useState(true)
    const [error, setError] = useState('')

    function getGender(gender) {
        return gender === 0 ? "Male" : "Female";
    }
    const navigate = useNavigate();

    useEffect(() => {
        async function fetchPatients() {
            const token = localStorage.getItem("MyToken");

            try {
                const response = await fetch(
                    `${API_BASE_URL}/api/Patient`,
                    {
                        headers: {
                            "Authorization": `Bearer ${token}`
                        }

                    }
                );
                if (response.ok) {
                    const data = await response.json();
                    setPatients(data);

                } else {
                    setError('Failed to load patients')
                }
            }
            catch {
                setError('Cannot reach the server');
            }
            finally {
                setLoading(false);
            }
        }
        fetchPatients();
    }
        , [])
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
                        {`Name: ${p.name}
                          ,Age: ${p.age}
                          ,Gender: ${(getGender(p.gender))}`}
                    </li>
                ))}
            </ul>

            <button type="button" onClick={() => navigate("/")}>Home</button>
            <button type="button" onClick={() => {handleLogOut()}}>LogOut</button>

        </div>
    )
    

}
export default PatientPage;