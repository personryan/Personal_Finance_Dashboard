export const FetchWithAuth = async (url: string, options: RequestInit = {}) => {
    let token = localStorage.getItem("token");

    const response = await fetch(url, {
        ...options,
        headers: {
            ...options.headers,
            Authorization: `Bearer ${token}` // FIXED HEADER NAME
        },
    });

    if (response.status === 401) { //JWT expired, attempt refresh
        console.warn("JWT expired, refreshing token...");

        const refreshToken = localStorage.getItem("refreshToken");
        if (!refreshToken) {
            console.error("No refresh token found. User needs to log in again.");
            localStorage.clear();
            window.location.href = "/login";
            return response;
        }

        // ✅ Attempt to refresh the token
        const refreshResponse = await fetch("http://localhost:5032/api/users/refresh", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ refreshToken }),
        });

        if (refreshResponse.ok) {
            const refreshData = await refreshResponse.json();
            console.log("New JWT issued:", refreshData.token);
            localStorage.setItem("token", refreshData.token);
            localStorage.setItem("refreshToken", refreshData.refreshToken);

            // ✅ Retry the original request with the new token
            return fetch(url, {
                ...options,
                headers: {
                    ...options.headers,
                    Authorization: `Bearer ${refreshData.token}` // FIXED HEADER NAME
                },
            });
        } else {
            console.error("Refresh token expired. Logging out...");
            localStorage.clear();
            window.location.href = "/login";
            return response;
        }
    }

    return response;
};
