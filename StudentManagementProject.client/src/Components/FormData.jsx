import nationalities from '../nationalities';
import PropTypes from 'prop-types';
import { useEffect, useState } from "react";

export default function FormData({ editStudent, addOrUpdateStudent }) {


    const [formData, setFormData] = useState({
        name: "",
        age: "",
        grade: "",
        gender: 1, // default to Male
        nationality: ""
    });

    const genders = [
        {label:"Male", value: 1},
        {label:"Female", value: 0},
    ]
    useEffect(() => {
        if (editStudent)
            setFormData(editStudent)
    },[editStudent])

    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData(prevData =>
            ({
                ...prevData,
                [name]: ["age", "grade", "gender"].includes(name) ? Number(value) : value
            })
        )

    };

    const handleSubmit = (e) => {
        e.preventDefault();
        addOrUpdateStudent(formData);
        setFormData({ name: "", age: "", grade: "", gender: 1, nationality: "" }); // Reset state
    }

    return (
        <div className="form-section">
            <h1>Students</h1>
            <h3>{editStudent ? "Edit Student" : "Add New Student"}</h3>
            <form className="input-gr" onSubmit={handleSubmit}>
                <label htmlFor="name">Name:</label>
                <input
                    id="name"
                    type="text"
                    name="name"
                    value={formData.name}
                    required
                    placeholder="Enter the name"
                    aria-label="Student name"
                    onChange={handleChange}

                />
                <label htmlFor="age">Age:</label>
                <input
                    id="age"
                    type="number"
                    name="age"
                    value={formData.age}
                    required
                    placeholder="Enter the age"
                    aria-label="Student age"
                    onChange={handleChange}
                />
                <label htmlFor="grade">Grade:</label>
                <input
                    id="grade"
                    type="number"
                    name="grade"
                    value={formData.grade}
                    required
                    placeholder="Enter the grade"
                    aria-label="Student grade"
                    onChange={handleChange}
                />
                <div className="radio-group">
                    <fieldset >
                        <legend>Gender:</legend>
                        {genders.map(g =>
                        (
                            <label key={g.label}>
                                <input
                                    type="radio"
                                    name="gender"
                                    value={g.value}
                                    checked={formData.gender === g.value}
                                    onChange={handleChange}
                                />
                                {g.label}
                            </label>
                        )) }
                    </fieldset>
                    <fieldset>
                        <legend>Nationality:</legend>
                        <select
                            id="nationality"
                            name="nationality"
                            value={formData.nationality}   
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Nationality</option>
                            {(nationalities || []).map((nat) => (
                                <option key={nat.id} >
                                    {nat.name}
                                </option>
                            ))}
                        </select>

                    </fieldset>
                </div>
                <button type="submit">{editStudent ? "Update Student" : "Add Student"}</button>
            </form>
        </div>

    )
}

FormData.propTypes = {
    editStudent: PropTypes.shape(
        {
            name: PropTypes.string,
            age: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            grade: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
            gender: PropTypes.oneOf([0, 1]),
            nationality: PropTypes.string,
        }),
    addOrUpdateStudent: PropTypes.func.isRequired,
};