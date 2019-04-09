using System.Collections.Generic;
using System.Linq;

namespace CustomerBarcodeGenerator
{
    class CustomerBarcode
    {
        public IEnumerable<Bar> Generate(string data)
        {
            return GenerateBarcodeCharacters(data).SelectMany(t => ConvertToBars(t));
        }

        private static IEnumerable<BarcodeCharacter> GenerateBarcodeCharacters(string data)
        {
            yield return BarcodeCharacter.Start;

            int sum = 19;
            foreach (var bc in PadLast(data.SelectMany(ConvertToBarcodeCharacters).Take(20), 20, BarcodeCharacter.CC4))
            {
                sum += (int)bc;
                yield return bc;
            }

            yield return BarcodeCharacter.Num0 + 18 - (sum - 1) % 19;
            yield return BarcodeCharacter.Stop;
        }

        private static IEnumerable<BarcodeCharacter> ConvertToBarcodeCharacters(char character)
        {
            if (character >= '0' && character <= '9')
            {
                yield return BarcodeCharacter.Num0 + character - '0';
            }
            else if (character == '-')
            {
                yield return BarcodeCharacter.Hyphen;
            }
            else if (character >= 'A' && character <= 'Z')
            {
                yield return BarcodeCharacter.CC1 + (character - 'A') / 10;
                yield return BarcodeCharacter.Num0 + (character - 'A') % 10;
            }
        }

        private static IEnumerable<Bar> ConvertToBars(BarcodeCharacter bc)
        {
            switch (bc)
            {
                case BarcodeCharacter.Num0:
                    yield return Bar.Long;
                    yield return Bar.Timing;
                    yield return Bar.Timing;
                    break;
                case BarcodeCharacter.Num1:
                    yield return Bar.Long;
                    yield return Bar.Long;
                    yield return Bar.Timing;
                    break;
                case BarcodeCharacter.Num2:
                    yield return Bar.Long;
                    yield return Bar.Down;
                    yield return Bar.Up;
                    break;
                case BarcodeCharacter.Num3:
                    yield return Bar.Down;
                    yield return Bar.Long;
                    yield return Bar.Up;
                    break;
                case BarcodeCharacter.Num4:
                    yield return Bar.Long;
                    yield return Bar.Up;
                    yield return Bar.Down;
                    break;
                case BarcodeCharacter.Num5:
                    yield return Bar.Long;
                    yield return Bar.Timing;
                    yield return Bar.Long;
                    break;
                case BarcodeCharacter.Num6:
                    yield return Bar.Down;
                    yield return Bar.Up;
                    yield return Bar.Long;
                    break;
                case BarcodeCharacter.Num7:
                    yield return Bar.Up;
                    yield return Bar.Long;
                    yield return Bar.Down;
                    break;
                case BarcodeCharacter.Num8:
                    yield return Bar.Up;
                    yield return Bar.Down;
                    yield return Bar.Long;
                    break;
                case BarcodeCharacter.Num9:
                    yield return Bar.Timing;
                    yield return Bar.Long;
                    yield return Bar.Long;
                    break;
                case BarcodeCharacter.Hyphen:
                    yield return Bar.Timing;
                    yield return Bar.Long;
                    yield return Bar.Timing;
                    break;
                case BarcodeCharacter.CC1:
                    yield return Bar.Down;
                    yield return Bar.Up;
                    yield return Bar.Timing;
                    break;
                case BarcodeCharacter.CC2:
                    yield return Bar.Down;
                    yield return Bar.Timing;
                    yield return Bar.Up;
                    break;
                case BarcodeCharacter.CC3:
                    yield return Bar.Up;
                    yield return Bar.Down;
                    yield return Bar.Timing;
                    break;
                case BarcodeCharacter.CC4:
                    yield return Bar.Timing;
                    yield return Bar.Down;
                    yield return Bar.Up;
                    break;
                case BarcodeCharacter.CC5:
                    yield return Bar.Up;
                    yield return Bar.Timing;
                    yield return Bar.Down;
                    break;
                case BarcodeCharacter.CC6:
                    yield return Bar.Timing;
                    yield return Bar.Up;
                    yield return Bar.Down;
                    break;
                case BarcodeCharacter.CC7:
                    yield return Bar.Timing;
                    yield return Bar.Timing;
                    yield return Bar.Long;
                    break;
                case BarcodeCharacter.CC8:
                    yield return Bar.Long;
                    yield return Bar.Long;
                    yield return Bar.Long;
                    break;
                case BarcodeCharacter.Start:
                    yield return Bar.Long;
                    yield return Bar.Down;
                    break;
                case BarcodeCharacter.Stop:
                    yield return Bar.Down;
                    yield return Bar.Long;
                    break;
                default:
                    break;
            }
        }

        private static IEnumerable<T> PadLast<T>(IEnumerable<T> src, int totalCount, T paddingValue)
        {
            int count = 0;
            foreach (var item in src)
            {
                count++;
                yield return item;
            }

            for (int i = count; i < totalCount; i++)
            {
                yield return paddingValue;
            }
        }

        private enum BarcodeCharacter
        {
            Num0,
            Num1,
            Num2,
            Num3,
            Num4,
            Num5,
            Num6,
            Num7,
            Num8,
            Num9,
            Hyphen,
            CC1,
            CC2,
            CC3,
            CC4,
            CC5,
            CC6,
            CC7,
            CC8,
            Start,
            Stop,
        }

        public enum Bar
        {
            Long = 1,
            Up = 2,
            Down = 3,
            Timing = 4,
        }
    }
}
