db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "Reservation"
            }
        ]
    }
);
db = new Mongo().getDB("Reservation");

db.createCollection("PlaceReservations");
