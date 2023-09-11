module.exports = {
    extends: ["@commitlint/config-conventional"],
    rules: {
      "type-enum": [2, "always", ["feat", "fix", "test", "chore", "refactor", "revert"]],
      "header-min-length": [2, "always", 10],
      "header-max-length": [2, "always", 50],
      "body-max-line-length": [2, "always", 250],
      "subject-case": [
        2,
        "never",
        ["sentence-case", "start-case", "pascal-case", "upper-case"],
      ],
    },
  }