db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "Checkpoint"
            }
        ]
    }
);
db = new Mongo().getDB("Checkpoint");

db.createCollection("Checkpoints");
