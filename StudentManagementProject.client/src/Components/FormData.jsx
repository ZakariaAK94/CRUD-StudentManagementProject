/* eslint-disable no-unused-vars */
/* eslint-disable react/prop-types */

import nationalities from '../nationalities';
import PropTypes from 'prop-types';
import { useState } from "react";
export default function FormData({ editStudent, addOrUpdateStudent }) {
    const [formData, setFormData] = useState({
        name: editStudent?.name || "",
        age: editStudent?.age || "",
        grade: editStudent?.grade || "",
        gender: editStudent?.gender || 1, // default to Male
        nationality: editStudent?.nationality || ""
    });
    console.log(editStudent?.gender || formData.gender)

    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData(prevData => ({ ...prevData, [name]: (name === "gender" ? parseInt(value) : value) }))

    };

    const handleSubmit = (e) => {
        e.preventDefault();
        addOrUpdateStudent(formData);
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
                    placeholder={editStudent ? editStudent.name : "Enter Name"}
                    onChange={handleChange}
                />
                <label htmlFor="age">Age:</label>
                <input
                    id="age"
                    type="number"
                    name="age"
                    value={formData.age}
                    required
                    placeholder={editStudent ? editStudent.age : "Enter Age"}
                    onChange={handleChange}
                />
                <label htmlFor="grade">Grade:</label>
                <input
                    id="grade"
                    type="number"
                    name="grade"
                    value={formData.grade}
                    required
                    placeholder={editStudent ? editStudent.grade : "Enter Grade"}
                    onChange={handleChange}
                />
                <div className="radio-group">
                    <fieldset >
                        <legend>Gender:</legend>
                        <label>
                            <input
                                type="radio"
                                name="gender"
                                value="1"
                                checked={editStudent ? editStudent.gender === 1 : formData.gender === 1}
                                onChange={handleChange}
                            />
                            Male
                        </label>
                        <label>
                            <input
                                type="radio"
                                name="gender"
                                value="0"
                                checked={editStudent ? editStudent.gender === 0 : formData.gender === 0}
                                onChange={handleChange}
                            />
                            Female
                        </label>
                    </fieldset>
                    <fieldset>
                        <legend>Nationality:</legend>
                        <select
                            id="nationality"
                            name="nationality"
                            value={editStudent ? editStudent.nationality : formData.nationality}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select Nationality</option>
                            {nationalities.map((nat) => (
                                <option key={nat.id} value={nat.name}>
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
            age: PropTypes.number,
            grade: PropTypes.number,
            gender: PropTypes.oneOf([0, 1]),
            nationality: PropTypes.string,
        }),
    addOrUpdateStudent: PropTypes.func.isRequired,
};