db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "Management"
            }
        ]
    }
);
db = new Mongo().getDB("Management");

db.createCollection("Accounts");
db.createCollection("PlaceFeatures");
