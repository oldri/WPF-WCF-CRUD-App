CREATE TABLE Accessories (
  AccessoryID int PRIMARY KEY,
  AccessoryName nvarchar(150),
  AccessoryPrice float,
  UnitsInStock smallint
);

INSERT INTO Accessories (AccessoryID, AccessoryName, AccessoryPrice, UnitsInStock)
VALUES (1, 'Brother Vellies for Fitbit 24mm Attach Woven Leather Bands', 74.95, 10),
       (2, 'Brother Vellies for Fitbit 24mm Attach Leather Scrunchies', 74.95, 5),
       (3, 'Victor Glemaud for Fitbit 24mm Attach Knit Bands', 49.95, 20);

CREATE TABLE SmartWatches (
  SmartWatchID int PRIMARY KEY,
  SmartWatchName nvarchar(150),
  SmartWatchPrice float,
  UnitsInStock smallint
);

INSERT INTO SmartWatches (SmartWatchID, SmartWatchName, SmartWatchPrice, UnitsInStock)
VALUES (1, 'Fitbit Sense 2', 399.95, 12),
       (2, 'Google Pixel Watch 4g LTE + Bluetooth / Wi-Fi, 7', 479.99, 7),
       (3, 'Fitbit Versa 4, 3', 259.95, 3);


CREATE TABLE Trackers (
  TrackerID int PRIMARY KEY,
  TrackerName nvarchar(150),
  TrackerPrice float,
  UnitsInStock smallint
);

INSERT INTO Trackers (TrackerID, TrackerName, TrackerPrice, UnitsInStock)
VALUES (1, 'Fitbit Charge 5', 199.95, 8),
       (2, 'Fitbit Luxe', 169.95, 25),
       (3, 'Fitbit Inspire 3', 129.95, 30);
