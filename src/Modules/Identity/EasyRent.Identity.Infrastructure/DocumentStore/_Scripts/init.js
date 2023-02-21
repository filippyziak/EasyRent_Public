db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "Identity"
            }
        ]
    }
);
db = new Mongo().getDB("Identity");

db.createCollection("Accounts");
