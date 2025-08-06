// קריאות ל-API
const URL = "http://localhost:5150/api";

// שמירה של ציור
export async function saveDrawing({ userId, name, prompt, commandJson }) {
    const res = await fetch(`${URL}/drawing`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userId, name, prompt, commandJson }),
    });

    if (!res.ok) {
        const errText = await res.text();
        throw new Error(errText);
    }

    return await res.json();
}

// טעינה של ציור
export async function loadDrawing(drawingId) {
    const res = await fetch(`${URL}/drawing/${drawingId}`);
    if (!res.ok) {
        throw new Error("שגיאה בטעינת הציור");
    }

    return await res.json();
}

//רישום משתמש
export async function registerUser({ username, password }) {
    const response = await fetch(`${URL}/user/register`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
        const message = await response.text();
        throw new Error(message || "שגיאה בהרשמה");
    }

    return await response.json();
}

//חיבור משתמש קיים
export async function loginUser({ username, password }) {
    const response = await fetch(`${URL}/user/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
        const message = await response.text();
        throw new Error(message || "שגיאה בהתחברות");
    }

    return await response.json();
}

//שולח פרומפט לשרת ומחזיר drawingCommands
export async function sendPrompt(prompt, prevDrawings) {
    const response = await fetch(`${URL}/prompt`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ prompt, prevDrawings }),
    });

    if (!response.ok) {
        throw new Error("שגיאה בשליחת פרומפט");
    }

    return await response.json();
}

//מחזיר את הציורים של המשתמש
export async function getUserDrawings(userId) {
    const response = await fetch(`${URL}/drawing/user/${userId}`);

    if (!response.ok) {
        throw new Error("שגיאה בקבלת ציורים של המשתמש");
    }

    return await response.json();
}