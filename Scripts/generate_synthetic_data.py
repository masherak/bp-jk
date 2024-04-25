import numpy as np
import pandas as pd

grade_improvement_factor = 8

def generate_synthetic_data(num_records=100000):
    num_per_major = num_records // 2  

    majors_courses = {
        "Business Management": ["Anglický jazyk", "Veřejné finance", "Finance", "Marketing", "Inovace v podnikání", "Lidské zdroje", "Management", "Mezinárodní obchod", "Podniková etika", "Projektový management", "Strategické řízení", "Účetnictví"],
        "Softwarový vývoj": ["Algoritmy", "Anglický jazyk", "Bezpečnost softwaru", "Cloud Computing", "Databáze", "Data Science", "Matematika", "Obchodní právo", "Operační systémy", "Programování", "Software Engineering", "Systémová architektura", "UI/UX Design", "Umělá inteligence", "Web Development"]
    }

    def generate_age(year):
        base_age = {1: 19, 2: 20, 3: 21}
        
        min_age = base_age[year]

        random_age = np.random.randint(19, 31) 
        adjusted_age = max(random_age, min_age) 
        
        return adjusted_age

    def generate_grade(year, age):
        age_adjustment = age - 19
        if age_adjustment == 0:
            grades_distribution = [1, 2, 2, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 5]
        else:
            grades_distribution = [1] * (1 + age_adjustment) + \
                                [2] * (2 + age_adjustment // 2) + \
                                [3] * (3 + age_adjustment // 3) + \
                                [4] * (4 + age_adjustment // 4) + \
                                [5] * (5 + age_adjustment // 5)

        base_weights = np.array([5, 4, 3, 2, 1])  
        base_weights = np.repeat(base_weights, [len([g for g in grades_distribution if g == i]) for i in range(1, 6)])

        improvement = 1 + grade_improvement_factor * (year - 1) + grade_improvement_factor * (age - 19)
        improved_weights = base_weights * improvement

        normalized_weights = improved_weights / sum(improved_weights)

        chosen_grade = np.random.choice(grades_distribution, p=normalized_weights)
        return chosen_grade
    
    ages, majors, years, courses, grades = [], [], [], [], []

    for major, courses_list in majors_courses.items():
        for _ in range(num_per_major):
            year = np.random.choice([1, 2, 3], 1)[0]
            course = np.random.choice(courses_list, 1)[0]
            age = generate_age(year)
            grade = generate_grade(year, age)

            ages.append(age)
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

data = generate_synthetic_data()

data.to_csv('synthetic_student_data.csv', index=False)
