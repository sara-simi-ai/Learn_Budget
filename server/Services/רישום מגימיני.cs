// public async Task<CourseRegistrationResponseDto> RegisterEmployeeToCourseAsync(CreateCourseRegistrationDto dto)
// {
//     // 1. שליפת הקורס והעובד מה-DB באמצעות Include (כי אנחנו עובדים בלי virtual)
//     var employee = await _context.Employees.FindAsync(dto.EmployeeId);
//     var course = await _context.Courses.FindAsync(dto.CourseId);

//     // ... כאן תבוא הלוגיקה שלך: בדיקת יתרת נקודות, בדיקת מקומות פנויים בקורס וכו' ...

//     // 2. ביצוע הרישום ועדכון הנקודות
//     employee.UsedCredits += course.CreditCost;
//     course.PlacesLeft -= 1;

//     var registration = new CourseRegistration
//     {
//         EmployeeId = employee.Id,
//         CourseId = course.Id,
//         RegistrationDate = DateTime.Now
//     };

//     _context.CourseRegistrations.Add(registration);
//     await _context.SaveChangesAsync(); // שמירה סופית בבסיס הנתונים

//     // 3. יצירת ה-DTO והרכבת התשובה (השילוב המנצח שחוסך קריאות ל-Frontend)
//     var response = new CourseRegistrationResponseDto
//     {
//         Id = registration.Id,
//         EmployeeId = registration.EmployeeId,
//         CourseId = registration.CourseId,
//         RegistrationDate = registration.RegistrationDate,
        
//         // כאן אנחנו "משאילים" נתונים מישות הקורס ומזריקים ל-DTO!
//         CourseTitle = course.Title,
//         CourseCreditCost = course.CreditCost,
        
//         // כאן אנחנו מזריקים את היתרה המעודכנת של העובד לאחר ההפחתה!
//         EmployeeAvailableCredits = employee.AvailableCredits 
//     };

//     return response;
// }