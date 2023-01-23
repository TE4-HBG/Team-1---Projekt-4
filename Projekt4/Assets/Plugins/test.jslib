{
    function GetScores() {
        const swag = localStorage.getItem("swag");
        return swag ? JSON.parse(swag) : [];
    }
    function SetScores(scores) {
        localStorage.setItem("swag", JSON.stringify(scores));
    }
    function AddScore(name, score) {
        const scores = GetScores();
        scores.push({name, score});
        SetScores(scores.sort((a, b) => b.score - a.score));
    }


    mergeInto(LibraryManager.library, {
      GetScores,
      SetScores,
      AddScore,
    });
}