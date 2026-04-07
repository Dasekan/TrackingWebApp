// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {

    // ---------- Session ----------
    function getOrCreateSessionId() {
        const key = "tracking_session_id";
        let sessionId = localStorage.getItem(key);

        if (!sessionId) {
            sessionId = crypto.randomUUID();
            localStorage.setItem(key, sessionId);
            console.log("Ny session oprettet:", sessionId);
        }
        return sessionId;
    }

    const sessionId = getOrCreateSessionId();

    // ---------- Send event ----------
    async function sendTrackingEvent(eventType, elementId) {
        const payload = {
            eventType: eventType,
            path: window.location.pathname,
            elementId: elementId || null,
            sessionId: sessionId,
            timestampUtc: new Date().toISOString()
        };

        try {
            const response = await fetch("/api/tracking/event", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(payload)
            });

            if (!response.ok) {
                console.warn("Tracking response not OK:", response.status);
                return;
            }

            // Vi læser ikke altid JSON for performance, men til debug er det fint:
            const data = await response.json();
            console.log("Tracking sendt:", data);
        }
        catch (err) {
            console.error("Tracking FEJL:", err);
        }
    }

    // ---------- Track pageview ----------
    sendTrackingEvent("pageview", null);

    // ---------- Track clicks for all marked elements ----------
    // Vi bruger event delegation, så det virker for alle links/knapper – også hvis du tilføjer nye senere.
    document.addEventListener("click", function (e) {
        const el = e.target.closest(".track-click");
        if (!el) return;

        // Prioritet: data-track -> id -> tekst
        const trackName =
            el.getAttribute("data-track") ||
            el.getAttribute("id") ||
            (el.textContent ? el.textContent.trim().slice(0, 50) : "unknown");

        // Send click event
        if (window.location.pathname.startsWith("/Admin")) return;
        sendTrackingEvent("click", trackName);
    });
});