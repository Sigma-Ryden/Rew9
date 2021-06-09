using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace Mathos.Parser
{
    public class MathParser
    {
        #region Properties

        /// <summary>
        /// This contains all of the binary operators defined for the parser.
        /// </summary>
        public Dictionary<string, Func<double, double, double>> Operators { get; set; }

        /// <summary>
        /// This contains all of the functions defined for the parser.
        /// </summary>
        public Dictionary<string, Func<double[], double>> LocalFunctions { get; set; }

        /// <summary>
        /// This contains all of the variables defined for the parser.
        /// </summary>
        public Dictionary<string, double> LocalVariables { get; set; }

        /// <summary>
        /// The culture information to use when parsing expressions.
        /// </summary>
        [Obsolete]
        public CultureInfo CultureInfo { get; set; }

        #endregion

        /// <summary>
        /// Constructs a new <see cref="MathParser"/> with optional functions, operators, and variables.
        /// </summary>
        /// <param name="loadPreDefinedFunctions">If true, the parser will be initialized with the functions abs, sqrt, pow,, sign, exp, floor, log, ln, trigonometric functions.</param>
        /// <param name="loadPreDefinedOperators">If true, the parser will be initialized with the operators ^, %, :, /, *, -, +.</param>
        /// <param name="loadPreDefinedVariables">If true, the parser will be initialized with the variables pi, e, phi.</param>
        /// <param name="cultureInfo">The culture information to use when parsing expressions. If null, the parser will use the invariant culture.</param>
        public MathParser(
            bool loadPreDefinedFunctions = true,
            bool loadPreDefinedOperators = true,
            bool loadPreDefinedVariables = true,
            CultureInfo cultureInfo = null)
        {
            if (loadPreDefinedOperators)
            {
                Operators = new Dictionary<string, Func<double, double, double>>
                {
                    ["^"] = Math.Pow,
                    ["%"] = (a, b) => a % b,
                    [":"] = (a, b) =>
                    {
                        if (b != 0) return a / b;
                        else if (a > 0) return double.PositiveInfinity;
                        else if (a < 0) return double.NegativeInfinity;
                        else return double.NaN;
                    },
                    ["/"] = (a, b) =>
                    {
                        if (b != 0) return a / b;
                        else if (a > 0) return double.PositiveInfinity;
                        else if (a < 0) return double.NegativeInfinity;
                        else return double.NaN;
                    },
                    ["*"] = (a, b) => a * b,
                    ["-"] = (a, b) => a - b,
                    ["+"] = (a, b) => a + b,
                };
            }
            else
            {
                Operators = new Dictionary<string, Func<double, double, double>>();
            }

            if (loadPreDefinedFunctions)
            {
                LocalFunctions = new Dictionary<string, Func<double[], double>>
                {
                    ["abs"] = inputs => Math.Abs(inputs[0]),
           
                    ["cos"] = inputs => Math.Cos(inputs[0]),
                    ["cosh"] = inputs => Math.Cosh(inputs[0]),
                    ["acos"] = inputs => Math.Acos(inputs[0]),
                    ["arccos"] = inputs => Math.Acos(inputs[0]),
                    
                    ["sin"] = inputs =>Math.Sin(inputs[0]),
                    ["sinh"] = inputs => Math.Sinh(inputs[0]),
                    ["asin"] = inputs => Math.Asin(inputs[0]),
                    ["arcsin"] = inputs => Math.Asin(inputs[0]),

                    ["tan"]  = inputs => Math.Tan(inputs[0]),
                    ["tanh"] = inputs => Math.Tanh(inputs[0]),
                    ["atan"] = inputs => Math.Atan(inputs[0]),
                    ["arctan"] = inputs => Math.Atan(inputs[0]),

                    ["sqrt"] = inputs => Math.Sqrt(inputs[0]),
                    ["pow"] = inputs => Math.Pow(inputs[0], inputs[1]),

                    ["sign"] = inputs => Math.Sign(inputs[0]),
                    ["exp"] = inputs => Math.Exp(inputs[0]),

                    ["floor"] = inputs => Math.Floor(inputs[0]),

                    ["log"] = inputs =>
                    {
                        switch (inputs.Length)
                        {
                            case 1: return Math.Log10(inputs[0]);
                            case 2: return Math.Log(inputs[0], inputs[1]);
                            default: return 0;
                        }
                    },

                    ["ln"] = inputs => Math.Log(inputs[0])
                };
            }
            else
            {
                LocalFunctions = new Dictionary<string, Func<double[], double>>();
            }

            if (loadPreDefinedVariables)
            {
                LocalVariables = new Dictionary<string, double>
                {
                    ["pi"] = 3.14159265358979,
                    ["e"] = 2.71828182845905,
                    ["phi"] = 1.61803398874989,
                };
            }
            else
            {
                LocalVariables = new Dictionary<string, double>();
            }

            CultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Parse and evaluate a mathematical expression.
        /// </summary>
        /// <remarks>
        /// This method does not evaluate variable declarations.
        /// For a method that does, please use <see cref="ProgrammaticallyParse"/>.
        /// </remarks>
        /// <example>
        /// <code>
        /// using System.Diagnostics;
        /// 
        /// var parser = new MathParser(false, true, false);
        /// Debug.Assert(parser.Parse("2 + 2") == 4);
        /// </code>
        /// </example>
        /// <param name="mathExpression">The math expression to parse and evaluate.</param>
        /// <returns>Returns the result of executing the given math expression.</returns>
        public double Parse(string mathExpression)
        {
            return MathParserLogic(Lexer($"{mathExpression}+0"));
        }

        #region Core
        private List<string> Lexer(string expr)
        {
            var token = "";
            var tokens = new List<string>();

            expr = expr.Replace("+-", "-");
            expr = expr.Replace("-+", "-");
            expr = expr.Replace($"-{LocalVariables.Last().Key}", $"-1*{LocalVariables.Last().Key}");
            expr = expr.Replace("--", "+");

            for (var i = 0; i < expr.Length; ++i)
            {
                var ch = expr[i];

                if (char.IsWhiteSpace(ch))
                {
                    continue;
                }

                if (char.IsLetter(ch))
                {
                    if (i != 0 && (char.IsDigit(expr[i - 1]) || expr[i - 1] == ')')) tokens.Add("*");

                    token += ch;

                    while (i + 1 < expr.Length && char.IsLetterOrDigit(expr[i + 1]))
                    {
                        token += expr[++i];
                    }                    

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (char.IsDigit(ch))
                {
                    token += ch;

                    while (i + 1 < expr.Length && (char.IsDigit(expr[i + 1]) || expr[i + 1] == '.'))
                    {
                        token += expr[++i];
                    }                    

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (ch == '.')
                {
                    token += ch;

                    while (i + 1 < expr.Length && char.IsDigit(expr[i + 1]))
                    {
                        token += expr[++i];
                    }

                    tokens.Add(token);
                    token = "";

                    continue;
                }

                if (ch == '(')
                {
                    if (i != 0 && (char.IsDigit(expr[i - 1]) || char.IsDigit(expr[i - 1]) || expr[i - 1] == ')'))
                    {
                        tokens.Add("*");
                        tokens.Add("(");
                    }
                    else
                    {
                        tokens.Add("(");
                    }
                }
                else
                {
                    tokens.Add(ch.ToString());
                }
            }

            return tokens;
        }

        private double MathParserLogic(List<string> tokens)
        {
            for (var i = 0; i < tokens.Count; ++i)
            {
                if (LocalVariables.Keys.Contains(tokens[i]))
                {
                    tokens[i] = LocalVariables[tokens[i]].ToString(CultureInfo);
                } 
            }

            while (tokens.IndexOf("(") != -1)
            {
                var open = tokens.LastIndexOf("(");
                var close = tokens.IndexOf(")", open);

                if (open >= close)
                {
                    throw new ArithmeticException($"No closing bracket/parenthesis.");
                }

                var roughExpr = new List<string>();

                for (var i = open + 1; i < close; ++i)
                {
                    roughExpr.Add(tokens[i]);
                }            

                double tmpResult;
                var args = new List<double>();
                var functionName = tokens[open == 0 ? 0 : open - 1];

                if (LocalFunctions.Keys.Contains(functionName))
                {
                    if (roughExpr.Contains(","))
                    {
                        // converting all arguments into a double array
                        for (var i = 0; i < roughExpr.Count; ++i)
                        {
                            var defaultExpr = new List<string>();
                            var firstCommaOrEndOfExpression =
                                roughExpr.IndexOf(",", i) != -1 ? roughExpr.IndexOf(",", i) : roughExpr.Count;

                            while (i < firstCommaOrEndOfExpression)
                            {
                                defaultExpr.Add(roughExpr[i++]);
                            }
                                
                            args.Add(defaultExpr.Count == 0 ? 0 : BasicArithmeticalExpression(defaultExpr));
                        }

                        // finally, passing the arguments to the given function
                        tmpResult = double.Parse(LocalFunctions[functionName](args.ToArray()).ToString(CultureInfo), CultureInfo);
                    }
                    else
                    {
                        if (roughExpr.Count == 0)
                            tmpResult = LocalFunctions[functionName](new double[0]);
                        else
                        {
                            tmpResult = double.Parse(LocalFunctions[functionName](new[]
                            {
                                BasicArithmeticalExpression(roughExpr)
                            }).ToString(CultureInfo), CultureInfo);
                        }
                    }
                }
                else
                {
                    // if no function is need to execute following expression, pass it
                    // to the "BasicArithmeticalExpression" method.
                    tmpResult = BasicArithmeticalExpression(roughExpr);
                }

                // when all the calculations have been done
                // we replace the "opening bracket with the result"
                // and removing the rest.
                tokens[open] = tmpResult.ToString(CultureInfo);
                tokens.RemoveRange(open + 1, close - open);

                if (LocalFunctions.Keys.Contains(functionName))
                {
                    // if we also executed a function, removing
                    // the function name as well.
                    tokens.RemoveAt(open - 1);
                }
            }

            // at this point, we should have replaced all brackets
            // with the appropriate values, so we can simply
            // calculate the expression. it's not so complex
            // any more
            return BasicArithmeticalExpression(tokens);
        }

        private double BasicArithmeticalExpression(List<string> tokens)
        {
            double token0;
            double token1;

            switch (tokens.Count)
            {
                case 1:
                    if (!double.TryParse(tokens[0], NumberStyles.Number, CultureInfo, out token0))
                    {
                        throw new MathParserException($"local variable {tokens[0]} is undefined.");
                    }
                    return token0;

                case 2:
                    var op = tokens[0];

                    if (op == "-" || op == "+")
                    {
                        var first = op == "+" ? "" : (tokens[1].Substring(0, 1) == "-" ? "" : "-");

                        if (!double.TryParse(first + tokens[1], NumberStyles.Number, CultureInfo, out token1)) return double.NaN;
                        return token1;
                    }
                    if (!double.TryParse(tokens[1], NumberStyles.Number, CultureInfo, out token1)) return double.NaN;
                    return Operators[op](0, token1);

                case 0:
                    return 0;
            }

            foreach (var op in Operators)
            {
                int opPlace;

                while ((opPlace = tokens.IndexOf(op.Key)) != -1)
                {
                    double rhs;

                    if (!double.TryParse(tokens[opPlace + 1], NumberStyles.Number, CultureInfo, out rhs))
                    {
                        if (tokens[opPlace + 1].Last() >= '0' && tokens[opPlace + 1].Last() <= '9')
                        {
                            return double.NaN; 
                        }
                        else
                        {
                            throw new MathParserException($"rhs local variable {tokens[opPlace + 1]} is undefined.");
                        }
                    }
                    

                    if (op.Key == "-" && opPlace == 0)
                    {
                        var result = op.Value(0.0, rhs);
                        tokens[0] = result.ToString(CultureInfo);
                        tokens.RemoveRange(opPlace + 1, 1);
                    }
                    else
                    {
                        double lhs;

                        if (!double.TryParse(tokens[opPlace - 1], NumberStyles.Number, CultureInfo, out lhs))
                        {
                            if (tokens[opPlace - 1].Last() >= '0' && tokens[opPlace - 1].Last() <= '9')
                            {
                                return double.NaN;
                            }
                            else
                            {
                                throw new MathParserException($"lhs local variable {tokens[opPlace - 1]} is undefined.");
                            }
                        }

                        var result = op.Value(lhs, rhs);
                        tokens[opPlace - 1] = result.ToString(CultureInfo);
                        tokens.RemoveRange(opPlace, 2);
                    }
                }
            }

            if (!double.TryParse(tokens[0], NumberStyles.Number, CultureInfo, out token0))
            {
                throw new MathParserException($"local variable {tokens[0]} is undefined.");
            }
            return token0;
        }

        #endregion
    }
}