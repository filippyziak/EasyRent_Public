db.createUser(
    {
        user: "admin",
        pwd: "admin1234",
        roles: [
            {
                role: "readWrite",
                db: "RentalAd"
            }
        ]
    }
);
db = new Mongo().getDB("RentalAd");

db.createCollection("RentalAds");
