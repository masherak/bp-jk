import numpy as np
import pandas as pd

def generate_synthetic_data(num_records=50000):
    num_per_major = num_records // 2  # Equal division between two majors

    # Major and courses information
    majors_courses = {
        "Business Management": ["Veřejné finance", "Finance", "Marketing", "Podnikové finance", "Marketingová komunikace"],
        "Softwarový vývoj": ["Algoritmy", "Obchodní právo", "Data Science", "Bezpečnost softwaru", "Objektově orientované programování"]
    }

    # Difficult courses in each major
    difficult_courses = {
        "Business Management": ["Veřejné finance", "Podnikové finance"],
        "Softwarový vývoj": ["Data Science", "Bezpečnost softwaru"]
    }

    def generate_age(year):
        base_age = {1: 19, 2: 20, 3: 21}
        return np.random.normal(base_age[year], 1, 1).astype(int)

    def generate_grade(course, year, major):
        if course in difficult_courses[major]:
            grades_distribution = [1, 2, 2, 3, 4, 5, 5]
        else:
            grades_distribution = [1, 1, 1, 2, 2, 3, 4]
        return np.random.choice(grades_distribution, 1)[0]

    ages, majors, years, courses, grades = [], [], [], [], []

    for major, courses_list in majors_courses.items():
        for _ in range(num_per_major):
            year = np.random.choice([1, 2, 3], 1)[0]
            course = np.random.choice(courses_list, 1)[0]
            grade = generate_grade(course, year, major)
            age = generate_age(year)

            ages.append(age[0])
            majors.append(major)
            years.append(year)
            courses.append(course)
            grades.append(grade)

    student_data = pd.DataFrame({
        "Věk": ages,
        "Studijní Obor": majors,
        "Ročník": years,
        "Předmět": courses,
        "Známka": grades
    })

    return student_data

# Generate data
data = generate_synthetic_data()

# Save to CSV
data.to_csv('synthetic_student_data.csv', index=False)
