namespace nights.test.skipintro;

public enum Gamemode {
	// GameMode_Progressive = 1, (black screen)
	// GameMode_MemoryCard = 2, (black screen)
	Saving = 3, // GameMode_MemoryCard
	Replay = 4, // GameMode_MemoryCard
	Advertise = 5, // GameMode_SegaLogo, game seems to start with this
	CutsceneIntro = 6, // GameMode_Movie
	CutsceneStoryBegin = 7, // GameMode_Demo
	Title = 8, // GameMode_Title
	// GameMode_LoadSelect = 9, (black screen)
	MainMenu = 10, // GameMode_ModeSelect
	DreamSelect = 11, // GameMode_SelectDiary
	PS2Option = 12, // GameMode_Option
	Gallery = 13, // GameMode_Gallery
	MovieTheatre = 14, // GameMode_MovieTheater
	NightopianCollection = 15, // &GameMode_Collection
	Quit = 18, // 
	GameplayButCrashes = 19, // GameMode_Game
	Replay2 = 20, // GameMode_Game
	CutsceneStoryEnd = 21, // GameMode_Ending
	CutsceneChristmasBegin = 22, // GameMode_Movie
	CutsceneChristmasEnd = 23, // GameMode_XmasEnding
	Hint = 24, // GameMode_Hint
	TrialUnlockFullGame = 25, // GameMode_TrialXBOX360
	PiaOver = 26, // GameMode_GameOver
	MareOver = 27, // GameMode_GameOver
	// GameMode_SizeChanger = 28 and 29
	GameplayRedirectNoCrash = 30, // GameMode_Game
	MainMenuRedirect = 31
	// GameMode_Logo_Loading = 32
}
