--CREATE TABLE Users (
--	UserId int primary key,
--	Username VARCHAR(20),
--	Email VARCHAR(255),
--	UserPassword VARCHAR(255),
--);

CREATE TABLE Recipes (
	RecipeId int primary key,
	UserId int foreign key references Users(UserId),
	RecipeName VARCHAR(20),
	RecipeDescription VARCHAR(255),
);

CREATE TABLE Ingredients (
	IngredientId int primary key,
	RecipeId int foreign key references Recipes(RecipeId),
	IngredientName VARCHAR(20),
	IngredientAmount VARCHAR(20),
);


CREATE TABLE Steps (
	StepId int primary key,
	RecipeId int foreign key references Recipes(RecipeId),
	StepDescription VARCHAR(255),
)


