import './App.css'
import Login from './components/Login'
import Register from './components/Register'
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Welcome from './components/Welcome';
import PatientPage from './components/PatientPage';

function App() {
    return (
        
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Welcome />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/patients" element={<PatientPage /> } />
            </Routes>
        </BrowserRouter>
    );
    }

export default App  