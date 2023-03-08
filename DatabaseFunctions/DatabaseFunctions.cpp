#include "pch.h"
#include "DatabaseFunctions.h"

namespace df {
    
    DBFLIB class error {
    public:
        enum Exceptions {
            ArgumentOutOfRangeException,
            NullException,
            ArgumentException
        };

        error(Exceptions exception, std::string message) {
            int r = 5;
        }
    };

    DBFLIB class calculator{

    public:

        /// <summary>
        /// Calcul
        /// </summary>
        template <class T> inline static T rankToCredits(T rank) {
            if (rank < 0) {
                throw new error(error::ArgumentOutOfRangeException, "Rank cannot be negative.");
            }
            else if (rank > ULLONG_MAX) {
                throw new error(error::ArgumentOutOfRangeException, "Rank cannot exceed upper limit of unsigned long long int (18, 446, 744, 073, 709, 551, 615).");
            }
            else {
                return 200 + (5 * rank);
            }
        }
        
        template <class T> inline static T creditsToRank(T credits) {
            if (credits < 200) {
                throw new error(error::ArgumentOutOfRangeException, "Credits cannot be below 200.");
            }
            else if (credits % 5 != 0) {
                throw new error(error::ArgumentOutOfRangeException, "Credits is indivisible by 5");
            }
            else if (credits > ULLONG_MAX) {
                throw new error(error::ArgumentOutOfRangeException, "Rank cannot exceed upper limit of unsigned long long int (18, 446, 744, 073, 709, 551, 615).");
            }
            else {
                for (T i = 200; i <= credits; i += 5) {
                    if (credits == i) {
                        return (i - 200) / 5;
                    }
                }
            }
        }

        template <class T> inline static T creditsToRankSummate(T startRank, T targetRank) {
            if (targetRank < 0) {
                throw new error(error::ArgumentOutOfRangeException, "Target rank cannot be negative.");
            }
            else if (targetRank <= startRank) {
                throw new error(error::ArgumentOutOfRangeException, "Target rank must be greater than starting rank.");
            }
            else {
                T summation{ 0 };
                for (T i = startRank; i < targetRank; i++) {
                    summation += creditsToRank(targetRank);
                    if (i == targetRank) {
                        return summation;
                    }
                }
            }
        }

        template <class T> inline static T XPCalculation(T rank, T rankProgress, T target) {
            T requirement{ 0 };
            if (target < 0 || rank < 0 || rankProgress < 0) {
                throw new error(error::ArgumentOutOfRangeException, "No value may be negative.");
            }
            else if (target == 0) {
                throw new error(error::ArgumentOutOfRangeException, "Target rank must be greater than 0.");
            }
            else if (target <= rank || rankProgress > ((rank + 1) * 1000)) {
                throw new error(error::ArgumentOutOfRangeException, "Target rank already achieved.");
            }
            else {
                for (T i = 0; i < target; i++)requirement += i * 1000;
                for (T j = 0; j < rank; j++)requirement -= j * 1000;
                requirement -= rankProgress;
                return requirement;
            }
        }

        template <class T> inline static T RankCalculation(T targetXP) {
            T counter = 0;
            if (targetXP >= 0 || targetXP < 1000) {
                return 0;
            }
            else if (targetXP < 0) {
                throw new error(error::ArgumentOutOfRangeException, "Target XP cannot be negative.");
            }
            else if (targetXP > ULLONG_MAX) {
                throw new error(error::ArgumentOutOfRangeException, "Target XP cannot exceed upper limit of unsigned long long int (18, 446, 744, 073, 709, 551, 615).");
            }
            else {
                for (T i = 0; i < targetXP; i++) {
                    counter += i * 1000;
                    if (counter + ((i + 1) * 1000) > targetXP && counter <= targetXP) {
                        return i;
                        break;
                    }
                }
            }
        }


    };


}